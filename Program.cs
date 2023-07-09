using Application.Configuration;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

var openAiApiOptions = builder.Configuration.GetSection("OpenAiApi") ?? throw new InvalidOperationException("OpenAiApi not found in configuration");
builder.Services.Configure<OpenAiApiOptions>(openAiApiOptions);

builder.Services.AddHttpClient<IGPTService, GPTService>();
builder.Services.AddTransient<IGPTService, GPTService>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors for local testing
// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(builder =>
//     {
//         builder.WithOrigins("http://10.0.2.2:5054")
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//     });
// });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();