using Microsoft.AspNetCore.SignalR;
using MQTTnet;
using Newtonsoft.Json;
using RemotePatientCare.IoT.Hubs;
using RemotePatientCare.IoT.Models;
using RemotePatientCare.IoT.Observers.IObservers;
using System.Text;

namespace RemotePatientCare.IoT.Observers
{
    public class RealTimePhysicalConditionObserver : IRealTimePhysicalConditionObserver
    {
        private readonly IHubContext<PhysicalConditionHub> _hub;
        public RealTimePhysicalConditionObserver(IHubContext<PhysicalConditionHub> hub)
        {
            _hub = hub;
        }

        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic1")
            {
                var payload = Encoding.UTF8.GetString(message.Payload);
                var physicalCondition = JsonConvert.DeserializeObject<IndicatorsPhysicalConditionRealTime>(payload)!;

                await _hub.Clients.Group($"{physicalCondition.PatientId}").SendAsync("PhysicalCondition", physicalCondition);
            }
        }
    }
}
