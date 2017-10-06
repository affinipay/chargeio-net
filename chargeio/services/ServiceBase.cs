using ChargeIO.Infrastructure;

namespace ChargeIo.services
{
    public class ServiceBase
    {
        protected string SecretKey { get; }

        protected ServiceBase(string secretKey = null)
        {
            SecretKey = secretKey ?? Configuration.SecretKey;
        }
    }
}