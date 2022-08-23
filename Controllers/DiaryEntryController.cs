using Microsoft.AspNetCore.Mvc;
using MyBlog.API.DTOs;
using MyBlog.API.Helpers;
using MyBlog.API.MongoDB.Models;
using MyBlog.API.MongoDB.Services;

namespace MyBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiaryEntryController : ControllerBase
    {
        private readonly ILogger<DiaryEntryController> _logger;
        private readonly IMongoDBService<DiaryEntry> _diaryEntryService;
        private readonly IMongoDBService<Diary> _diaryService;

        public DiaryEntryController(ILogger<DiaryEntryController> logger, IMongoDBService<DiaryEntry> diaryEntryService, IMongoDBService<Diary> diaryService)
        {
            _logger = logger;
            _diaryEntryService = diaryEntryService;
            _diaryService = diaryService;
        }

        [HttpGet] 
        public async Task<IEnumerable<DiaryEntryDTO>> GetDiaryEntriesAsync()
        {
            var diaryEntries = (await _diaryEntryService.GetItemsAsync())
                            .Select(item => item.AsDTO());

            return diaryEntries;
        }

        // GET /DiaryEntry/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntryDTO>> GetDiaryEntryAsync(Guid id)
        {
            var diaryEntry = await _diaryEntryService.GetItemAsync(id);

            if (diaryEntry == null)
            {
                return NotFound();
            }

            return diaryEntry.AsDTO();
        }

        // POST /CreateDiaryEntry
        [HttpPost]
        public async Task<ActionResult<DiaryEntryDTO>> CreateDiaryEntryAsync(Guid diaryId,DiaryEntryCreateDTO itemCreateDTO) 
        {
            var diaryEntry = new DiaryEntry()
            {
                Id = Guid.NewGuid(),
                DiaryEntryTitle = itemCreateDTO.DiaryEntryTitle,
                DiaryEntryDescription = itemCreateDTO.DiaryEntryDescription,
                DiaryEntryType = itemCreateDTO.DiaryEntryType,
                DiaryId = diaryId,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _diaryEntryService.CreateItemAsync(diaryEntry);

            var diary = await _diaryService.GetItemAsync(diaryId);
            diary.DiaryEntries.Add(diaryEntry.Id);

            await _diaryService.UpdateItemAsync(diary);


            return CreatedAtAction(nameof(GetDiaryEntryAsync), new { id = diaryEntry.Id }, diaryEntry.AsDTO());
        }

        // PUT /UpdateDiaryEntry/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDiaryEntryAsync(Guid id, DiaryEntryUpdateDTO diaryEntryUpdateDTO)
        {
            var currentDiaryEntry = await _diaryEntryService.GetItemAsync(id);

            if (currentDiaryEntry is null)
            {
                return NotFound();
            }

            DiaryEntry updatedItem = currentDiaryEntry with
            {
                DiaryEntryTitle = diaryEntryUpdateDTO.DiaryEntryTitle,
                DiaryEntryDescription = diaryEntryUpdateDTO.DiaryEntryDescription,
                DiaryEntryType = diaryEntryUpdateDTO.DiaryEntryType
            };

            await _diaryEntryService.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        // DELETE /DiaryEntry/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiaryEntryAsync(Guid id)
        {
            var existingDiaryEntry = await _diaryEntryService.GetItemAsync(id);

            if (existingDiaryEntry is null)
            {
                return NotFound();
            }

            await _diaryEntryService.DeleteItemAsync(id);

            var diary = await _diaryService.GetItemAsync(existingDiaryEntry.DiaryId);
            diary.DiaryEntries.Remove(id);

            await _diaryService.UpdateItemAsync(diary);


            return NoContent();
        }
    }
}
