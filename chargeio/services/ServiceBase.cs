using ChargeIO.Infrastructure;

namespace ChargeIO.services
{
    public abstract class ServiceBase
    {
        protected string SecretKey { get; }
        
        protected ServiceBase(string secretKey)
        {
            SecretKey = string.IsNullOrEmpty(secretKey) ? Configuration.SecretKey : secretKey;
        }
    }
}