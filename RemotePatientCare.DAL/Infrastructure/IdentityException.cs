namespace RemotePatientCare.DAL.Infrastructure
{
    public class IdentityException : ApplicationException
    {
        public IdentityException(string message) : base(message) { }
    }
}
