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
    public class PhysicalConditionRealTimeObserver : IPhysicalConditionRealTimeObserver
    {
        private readonly IHubContext<PhysicalConditionHub> _hub;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public PhysicalConditionRealTimeObserver(IHubContext<PhysicalConditionHub> hub, IServiceScopeFactory serviceScopeFactory)
        {
            _hub = hub;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic1")
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var _patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();


                var payload = Encoding.UTF8.GetString(message.Payload);
                var physicalCondition = JsonConvert.DeserializeObject<IndicatorsPhysicalConditionRealTime>(payload)!;

                var caretaker = await _patientService.GetPatientCaretakerAsync(physicalCondition.PatientId);
                var doctor = await _patientService.GetPatientDoctorAsync(physicalCondition.PatientId);

                await _hub.Clients.All.SendAsync("PhysicalCondition", physicalCondition);
            }
        }
    }
}
