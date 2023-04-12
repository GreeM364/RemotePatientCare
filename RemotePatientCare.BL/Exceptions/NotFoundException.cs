namespace RemotePatientCare.BLL.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message) {}
    }
}
