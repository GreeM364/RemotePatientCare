using MQTTnet;
using RemotePatientCare.IoT.Observers.IObservers;
using System.Text;

namespace RemotePatientCare.IoT.Observers
{
    public class CriticalСonditionObserver : ICriticalСonditionObserver
    {
        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            if (message.Topic == "test/topic3")
            {
                var payload = Encoding.UTF8.GetString(message.Payload);

                // Обработка сообщения от топика "test/topic3"
            }
        }
    }
}
