using ApiVersionNeutralBugExample;
using Asp.Versioning;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new HeaderApiVersionReader("api-version");
})
            .AddMvc()
            .AddApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        // add a custom operation filter which sets default values
        options.OperationFilter<SwaggerDefaultValues>();

        //var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
        //var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        // integrate xml comments
        //options.IncludeXmlComments(filePath);
    }); ;
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
         options =>
         {
             var descriptions = app.DescribeApiVersions();

             // build a swagger endpoint for each discovered API version
             foreach (var description in descriptions)
             {
                 var url = $"/swagger/{description.GroupName}/swagger.json";
                 var name = description.GroupName.ToUpperInvariant();
                 options.SwaggerEndpoint(url, name);
             }
         });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
