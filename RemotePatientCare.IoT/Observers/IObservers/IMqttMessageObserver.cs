using MQTTnet;

namespace RemotePatientCare.IoT.Observers.IObservers
{
    public interface IMqttMessageObserver
    {
        Task HandleMessageAsync(MqttApplicationMessage message);
    }
}
