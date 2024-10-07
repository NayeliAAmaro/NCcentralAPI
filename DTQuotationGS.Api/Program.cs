
using DTQuotationGS.Business;
using DTQuotationGS.Entities.commons;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<YourConfigClass>(builder.Configuration.GetSection("ApiSettings"));

NovaCajaConfig novaCajaConfig = builder.Configuration.GetSection("ApiSettings:Apis").Get<NovaCajaConfig>();
builder.Services.AddSingleton(novaCajaConfig);


builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton(novaCajaConfig);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // In non-development environments, you might not want to expose Swagger.
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

