using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.OpenApi.Models;
using Sofisoft.Enterprise.Logging.Api.Infrastructure.AutofacModules;
using Sofisoft.Enterprise.Logging.Api.Infrastructure.Handlers;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Config dataprotection
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDataProtection()
        .UseEphemeralDataProtectionProvider();
}
else
{
    builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"/var/lib/dataprotected/"))
    .ProtectKeysWithCertificate(new X509Certificate2(
        Convert.FromBase64String(configuration["Certificate:Content"]),
        configuration["Certificate:Password"])
    );
}

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder => 
{
    builder.RegisterModule(new MediatorModule());
});

// Add services to the container.
builder.Services.AddSofisoft()
    .AddMongoDb(options =>
    {
        options.SetConnectionString(configuration["ConnectionString"]);
        options.SetDatabase(configuration["Database"]);
    });

builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BasicAuthentication",
        new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    {
        Title = "Logging API",
        Version = "v1",
        Description = "Specifying services for logging.",
        Contact = new OpenApiContact
        {
            Email = "juan.abanto@sofisoft.pe",
            Name = "Sofisoft Technologies SAC",
            Url = new Uri(configuration["Swagger:HomePage"])
        }
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Sofisoft - LoggingAPI";
        c.RoutePrefix = string.Empty;
        c.SupportedSubmitMethods(Array.Empty<SubmitMethod>());
        c.SwaggerEndpoint(configuration["Swagger:EndPoint"], "Looging Api V1");
        c.DefaultModelsExpandDepth(-1);
    });

app.Run();

// using (var scope = builder.Services.ser)
// //Initialize enumerables MongoDB
// foreach (var data in EventLogType.List())
// {
//     dataContext.EventLogTypes.FindOneAndUpdateAsync(
//         Builders<EventLogType>.Filter.Eq(b => b.Id, data.Id),
//         Builders<EventLogType>.Update.Set(b => b.Name, data.Name),
//         new FindOneAndUpdateOptions<EventLogType> { ReturnDocument = ReturnDocument.After, IsUpsert = true }
//     );
// }
