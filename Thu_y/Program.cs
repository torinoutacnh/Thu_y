using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Thu_y;
using Thu_y.Db.DbContext;
using Thu_y.Extensions;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.UOF;
using Thu_y.Middleware;
using Thu_y.Modules.AbttoirModule.Model.Mapper;
using Thu_y.Modules.ReceiptModule.Model.Mapper;
using Thu_y.Modules.ReportModule.Model.Mapper;
using Thu_y.Modules.ShareModule.Model.Mapper;
using Thu_y.Modules.UserModule.Model.Mapper;
using Thu_y.Utils.Infrastructure.Application.Models;
using Thu_y.Utils.Module;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "AddAuthorization",
        Description = "Lấy Bearer token mới bắn được API nha mấy nhóc!",
        Type = SecuritySchemeType.Http
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Id = "Bearer",
                     Type = ReferenceType.SecurityScheme
                 }
             },
             new List<string>()
        }
    });
    o.SwaggerDoc("v1",new OpenApiInfo { Title = "Thu_Y.API",Version = "v1"});
});
builder.Services.AddSystemSetting(builder.Configuration.GetSection("SystemHelper").Get<SystemHelperModel>());
builder.Services.AddJwtSetting(builder.Configuration.GetSection("JwtSetting").Get<JWTSettingModel>());
builder.Services.AddMailSetting(builder.Configuration.GetSection("MailSetting").Get<MailSettingModel>());

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IDbContext, AppDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new UserModuleProfile());
    cfg.AddProfile(new FormMapperProfile());
    cfg.AddProfile(new ShareModuleProfile());
    cfg.AddProfile(new ReceiptModuleProfile());
    cfg.AddProfile(new AbttoirModuleProfile());
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<RouteOptions>(options =>
{
    options.AppendTrailingSlash = false;
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = false;
});
builder.Services.AddAuth();
builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
builder.Services.AddCors(options =>
{
    options.AddPolicy("LongCors", builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
builder.Services.AddAuthorization();

builder.Services.RegisterModules();

var app = builder.Build();

//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
var swagger = builder.Configuration.GetValue("UseSwagger", false);
if(swagger)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thu_Y.API v1");
    });
}

app.UseSystemSetting();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("LongCors");
app.UseHttpLogging();
app.UseAuthorization();
app.UseAuthentication();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.MapEndpoints();
app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
