using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MyBlog.API.MongoDB.Models;
using MyBlog.API.MongoDB.Services;

var builder = WebApplication.CreateBuilder(args);
var mongoDBSettings = builder.Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();

// MongoDB settings
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));


// Add services to the container.
ConfigureServices(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultControllerRoute();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();



void ConfigureServices(IServiceCollection services)
{

    services.AddSingleton<IMongoClient>(serviceProvider =>
    {
        return new MongoClient(mongoDBSettings.ConnectionString);
    });
    services.AddSingleton<IMongoDBService<User>,MongoDBUserService>();
    services.AddSingleton<IMongoDBService<Diary>, MongoDBDiaryService>();
    services.AddSingleton<IMongoDBService<DiaryEntry>, MongoDBDiaryEntryService>();
    services.AddEndpointsApiExplorer();
    services.AddControllers();
    services.AddSwaggerGen();
}

