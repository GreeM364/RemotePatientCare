namespace RemotePatientCare.IoT.Models
{
    public class MqttSeting
    {
        public string BrokerAddress { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ClientId { get; set; } = null!;
    }
}
