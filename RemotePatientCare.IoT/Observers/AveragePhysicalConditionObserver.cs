using AutoMapper;
using MQTTnet;
using Newtonsoft.Json;
using RemotePatientCare.BLL.DataTransferObjects;
using RemotePatientCare.BLL.Services;
using RemotePatientCare.BLL.Services.Interfaces;
using RemotePatientCare.IoT.Models;
using RemotePatientCare.IoT.Observers.IObservers;
using System.Text;

namespace RemotePatientCare.IoT.Observers
{
    public class AveragePhysicalConditionObserver : IAveragePhysicalConditionObserver
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        public AveragePhysicalConditionObserver(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }
        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic2")
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var _physicalConditionService = scope.ServiceProvider.GetRequiredService<IPhysicalConditionService>();


                var payload = Encoding.UTF8.GetString(message.Payload);
                var physicalCondition = JsonConvert.DeserializeObject<IndicatorsPhysicalConditionAverage>(payload);

                var physicalConditionDTO = _mapper.Map<PhysicalConditionCreateDTO>(physicalCondition);
                await _physicalConditionService.CreateAsync(physicalConditionDTO);
            }
        }
    }
}
