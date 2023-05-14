using Microsoft.AspNetCore.SignalR;
using MQTTnet;
using Newtonsoft.Json;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.IoT.Hubs;
using RemotePatientCare.IoT.Models;
using RemotePatientCare.IoT.Observers.IObservers;
using System.Text;

namespace RemotePatientCare.IoT.Observers
{
    public class CriticalСonditionObserver : ICriticalСonditionObserver
    {
        private readonly IHubContext<PhysicalConditionHub> _hub;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public CriticalСonditionObserver(IHubContext<PhysicalConditionHub> hub, IServiceScopeFactory serviceScopeFactory)
        {
            _hub = hub;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic3")
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var _patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();


                var payload = Encoding.UTF8.GetString(message.Payload);
                var criticalCondition = JsonConvert.DeserializeObject<IndicatorsCriticalСondition>(payload)!;

                var caretaker = await _patientService.GetPatientCaretakerAsync(criticalCondition.PatientId);
                var doctor = await _patientService.GetPatientDoctorAsync(criticalCondition.PatientId);

                await _hub.Clients.All.SendAsync("CriticalCondition", criticalCondition);
            }
        }
    }
}
