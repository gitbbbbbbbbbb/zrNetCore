using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using ZR.Admin.WebApi.Framework;
using Hei.Captcha;
using Infrastructure.Extensions;
using ZR.Admin.WebApi.Extensions;
using ZR.Admin.WebApi.Filters;
using ZR.Admin.WebApi.Middleware;
using ZR.Admin.WebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ע��HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//���ÿ���
builder.Services.AddCors(c =>
{
    c.AddPolicy("Policy", policy =>
    {
        policy.WithOrigins(builder.Configuration["corsUrls"].Split(',', StringSplitOptions.RemoveEmptyEntries))
        .AllowAnyHeader()//��������ͷ
        .AllowCredentials()//����cookie
        .AllowAnyMethod();//�������ⷽ��
    });
});

//ע��SignalRʵʱͨѶ��Ĭ����json����
builder.Services.AddSignalR();
//����Error unprotecting the session cookie����
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "DataProtection"));
//��ͨ��֤��
builder.Services.AddHeiCaptcha();
//builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
//����������Model��
builder.Services.Configure<OptionsSetting>(builder.Configuration);

//jwt ��֤
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie()
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = JwtUtil.ValidParameters();
});

InternalApp.InternalServices = builder.Services;
builder.Services.AddAppService();
builder.Services.AddSingleton(new AppSettings(builder.Configuration));
//�����ƻ�����
builder.Services.AddTaskSchedulers();
//��ʼ��db
DbExtension.AddDb(builder.Configuration);

//ע��REDIS ����
Task.Run(() =>
{
    //RedisServer.Initalize();
});
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(GlobalActionMonitor));//ȫ��ע��
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonConverterUtil.DateTimeConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonConverterUtil.DateTimeNullConverter());
});

builder.Services.AddSwaggerConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//ʹ���Զ�ζ�ȥbody����
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    if (context.Request.Query.TryGetValue("access_token", out var token))
    {
        context.Request.Headers.Add("Authorization", $"Bearer {token}");
    }
    return next();
});
//�������ʾ�̬�ļ�/wwwrootĿ¼�ļ���Ҫ����UseRoutingǰ��
app.UseStaticFiles();
//����·�ɷ���
app.UseRouting();
app.UseCors("Policy");//Ҫ����app.UseEndpointsǰ��
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//��������
app.UseResponseCaching();
//�ָ�/��������
app.UseAddTaskSchedulers();
//ʹ��ȫ���쳣�м��
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseEndpoints(endpoints =>
{
    //����socket����
    endpoints.MapHub<MessageHub>("/msgHub");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.MapControllers();

app.Run();