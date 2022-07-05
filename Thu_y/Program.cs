using AutoMapper;
using Thu_y.Db.DbContext;
using Thu_y.Infrastructure.DbContext;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReportModule.Model.Mapper;
using Thu_y.Modules.ShareModule.Model.Mapper;
using Thu_y.Modules.UserModule.Model.Mapper;
using Thu_y.Utils.Infrastructure.Application;
using Thu_y.Utils.Infrastructure.Application.Models;
using Thu_y.Utils.Module;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSystemSetting(builder.Configuration.GetSection("SystemHelper").Get<SystemHelperModel>());

builder.Services.AddDbContext<IDbContext, AppDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new UserModuleProfile());
    cfg.AddProfile(new FormMapperProfile());
    cfg.AddProfile(new ShareModuleProfile());
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<RouteOptions>(options => {
    options.AppendTrailingSlash = false;
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = false;
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.RegisterModules();

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
app.UseHttpLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();
