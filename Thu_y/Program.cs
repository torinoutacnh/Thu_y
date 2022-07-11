using AutoDependencyRegistration;
using Thu_y;
using Thu_y.Db.DbContext;
using Thu_y.Extensions;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.AbttoirModule.Model.Mapper;
using Thu_y.Modules.ReceiptModule.Model.Mapper;
using Thu_y.Modules.ReportModule.Model.Mapper;
using Thu_y.Modules.ShareModule.Model.Mapper;
using Thu_y.Modules.UserModule.Model.Mapper;
using Thu_y.Utils.Infrastructure.Application.Models;
using Thu_y.Utils.Module;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSystemSetting(builder.Configuration.GetSection("SystemHelper").Get<SystemHelperModel>());
builder.Services.AddJwtSetting(builder.Configuration.GetSection("JwtSetting").Get<JWTSettingModel>());

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
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "LongPolicy",
//                      policy =>
//                      {
//                          policy.WithOrigins("https://apithuy72.amazingtech.vn",
//                                              "https://amazingtech.vn",
//                                              "https://localhost:3000",
//                                              "http://localhost:3000")
//                            .AllowAnyMethod()
//                            .AllowAnyHeader()
//                            .SetIsOriginAllowed(hostname => true)
//                            .SetIsOriginAllowedToAllowWildcardSubdomains();
//                      });


//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("LongCors", builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
builder.Services.AddAuthorization();

builder.Services.RegisterModules();
//builder.Services.AutoRegisterDependencies();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();
app.UseSystemSetting();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("LongCors");
app.UseHttpLogging();
app.UseAuthorization();
app.UseAuthentication();

app.MapEndpoints();

app.Run();
