using Microsoft.AspNetCore.Mvc;
using MyBlog.API.DTOs;
using MyBlog.API.Helpers;
using MyBlog.API.MongoDB.Models;
using MyBlog.API.MongoDB.Services;

namespace MyBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiaryController : ControllerBase
    {
        private readonly ILogger<DiaryController> _logger;
        private readonly IMongoDBService<Diary> _diaryService;
        private readonly IMongoDBService<DiaryEntry> _diaryEntryService;
        public DiaryController(ILogger<DiaryController> logger, IMongoDBService<Diary> diaryService, IMongoDBService<DiaryEntry> diaryEntryService)
        {
            _logger = logger;
            _diaryService = diaryService;
            _diaryEntryService = diaryEntryService;
        }


        // GET /Diary
        [HttpGet]
        public async Task<IEnumerable<DiaryDTO>> GetDiariesAsync()
        {
            var diaries = await _diaryService.GetItemsAsync();

            var diariesDto = new List<DiaryDTO>();

            foreach (var diary in diaries)
            {
                var diaryDTO = diary.AsDTO();

                foreach (var diaryEntry in diary.DiaryEntries)
                {
                    var entry = await _diaryEntryService.GetItemAsync(diaryEntry);
                    diaryDTO.DiaryEntries.Add(entry.AsDTO());
                }

                diariesDto.Add(diaryDTO);
            }

            return diariesDto;
        }


        // GET /Diary/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryDTO>> GetDiaryAsync(Guid id)
        {
            var diary = await _diaryService.GetItemAsync(id);

            if (diary == null)
            {
                return NotFound();
            }

            var diaryDTO = diary.AsDTO();

            foreach(var diaryEntryID in diary.DiaryEntries)
            {
                var entry = await _diaryEntryService.GetItemAsync(diaryEntryID);
                diaryDTO.DiaryEntries.Add(entry.AsDTO());
            }


            return diaryDTO;
        }


        // POST /CreateDiary
        [HttpPost]
        public async Task<ActionResult<DiaryDTO>> CreateDiaryAsync(DiaryCreateDTO itemCreateDTO)
        {
            var diary = new Diary() 
            {
                Id = Guid.NewGuid(),
                DiaryName = itemCreateDTO.DiaryName,
                DiaryDescription = itemCreateDTO.DiaryDescription,
                DiaryImage = itemCreateDTO.DiaryImage,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _diaryService.CreateItemAsync(diary);

            return CreatedAtAction(nameof(GetDiaryAsync), new { id = diary.Id }, diary.AsDTO());
        }


        // TODO: Update diary logic
        // PUT /UpdateDiary/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDiaryAsync(Guid id, DiaryUpdateDTO diaryUpdateDTO)
        {
            var currentDiary = await _diaryService.GetItemAsync(id);

            if (currentDiary is null)
            {
                return NotFound();
            }

            Diary updatedItem = currentDiary with
            {
                DiaryName = diaryUpdateDTO.DiaryName,
                DiaryDescription = diaryUpdateDTO.DiaryDescription,
                DiaryImage = diaryUpdateDTO.DiaryImage,
            };

            await _diaryService.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiaryAsync(Guid id)
        {
            var existingDiary = await _diaryService.GetItemAsync(id);

            if (existingDiary is null)
            {
                return NotFound();
            }

            if (existingDiary.DiaryEntries is not null)
            {
                foreach (var item in existingDiary.DiaryEntries)
                {
                    await _diaryEntryService.DeleteItemAsync(item);
                }
            }

            await _diaryService.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
