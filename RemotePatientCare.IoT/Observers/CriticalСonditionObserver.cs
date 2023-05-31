using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using MQTTnet;
using Newtonsoft.Json;
using RemotePatientCare.BLL.DataTransferObjects;
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
        private readonly IMapper _mapper;
        public CriticalСonditionObserver(IHubContext<PhysicalConditionHub> hub, IServiceScopeFactory serviceScopeFactory,
                                         IMapper mapper)
        {
            _hub = hub;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic3")
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var payload = Encoding.UTF8.GetString(message.Payload);
                var criticalCondition = JsonConvert.DeserializeObject<IndicatorsCriticalСondition>(payload)!;

                await Send(criticalCondition);
                await Save(scope, criticalCondition);
            }
        }

        private async Task Save(IServiceScope scope, IndicatorsCriticalСondition criticalCondition)
        {
            var criticalСonditionService = scope.ServiceProvider.GetRequiredService<ICriticalСonditionService>();
            var criticalConditionDTO = _mapper.Map<CriticalСonditionCreateDTO>(criticalCondition);

            await criticalСonditionService.CreateAsync(criticalConditionDTO);
        }

        private async Task Send(IndicatorsCriticalСondition criticalCondition)
        {
            await _hub.Clients.Group($"{criticalCondition.PatientId}").SendAsync("CriticalCondition", criticalCondition);
        }
    }
}
