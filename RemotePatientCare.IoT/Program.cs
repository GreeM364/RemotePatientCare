using RemotePatientCare.BLL.Infrastructure;
using RemotePatientCare.IoT;
using RemotePatientCare.IoT.Hubs;
using RemotePatientCare.IoT.Observers;
using RemotePatientCare.IoT.Observers.IObservers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(AutomapperIoTProfile));
builder.Services.AddBusinessLogicLayer(builder.Configuration);

builder.Services.AddSingleton<IRealTimePhysicalConditionObserver, RealTimePhysicalConditionObserver>();
builder.Services.AddSingleton<IAveragePhysicalConditionObserver, AveragePhysicalConditionObserver>();
builder.Services.AddSingleton<ICriticalÑonditionObserver, CriticalÑonditionObserver>();
builder.Services.AddHostedService<MqttService>();


var app = builder.Build();

app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<PhysicalConditionHub>("/physicalconditionhub");

app.Run();
