using ChargeIO.Infrastructure;

namespace ChargeIO.services
{
    public abstract class ServiceBase
    {
        protected string SecretKey { get; }
        
        protected ServiceBase(string secretKey)
        {
            SecretKey = secretKey == null || secretKey.Equals(string.Empty) ? Configuration.SecretKey : secretKey;
        }
    }
}