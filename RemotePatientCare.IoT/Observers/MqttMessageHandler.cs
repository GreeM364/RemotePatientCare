using MQTTnet;
using RemotePatientCare.IoT.Observers.IObservers;

namespace RemotePatientCare.IoT.Observers
{
    public class MqttMessageHandler
    {
        private readonly List<IMqttMessageObserver> _observers;

        public MqttMessageHandler()
        {
            _observers = new List<IMqttMessageObserver>();
        }

        public void RegisterObserver(IMqttMessageObserver observer)
        {
            _observers.Add(observer);
        }

        public async Task HandleMessageAsync(MqttApplicationMessage message)
        {
            foreach (var observer in _observers)
            {
                await observer.HandleMessageAsync(message);
            }
        }
    }

}
