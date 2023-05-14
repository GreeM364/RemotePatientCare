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

builder.Services.AddScoped<IPhysicalConditionRealTimeObserver, PhysicalConditionRealTimeObserver>();
builder.Services.AddScoped<IAveragePhysicalConditionObserver, AveragePhysicalConditionObserver>();
builder.Services.AddScoped<ICritical—onditionObserver, Critical—onditionObserver>();
builder.Services.AddHostedService<MqttService>(provider =>
{
    var options = provider.GetService<IConfiguration>();
    using (var scope = provider.CreateScope())
    {
        var physicalConditionRealTimeObserver = scope.ServiceProvider.GetService<IPhysicalConditionRealTimeObserver>();
        var averagePhysicalConditionObserver = scope.ServiceProvider.GetService<IAveragePhysicalConditionObserver>();
        var critical—onditionObserver = scope.ServiceProvider.GetService<ICritical—onditionObserver>();
        return new MqttService(options, physicalConditionRealTimeObserver, averagePhysicalConditionObserver, critical—onditionObserver);
    }
});
//builder.Services.BuildServiceProvider().GetService<MqttService>().ConnectAsync();


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

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<PhysicalConditionHub>("/physicalconditionhub");
    endpoints.MapControllers();
});

app.Run();
