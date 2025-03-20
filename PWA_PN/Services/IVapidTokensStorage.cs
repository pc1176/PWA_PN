namespace PWA_PN.Services
{
    public interface IVapidTokensStorage
    {
        string PublicKey { get; }

        string PrivateKey { get; }
    }

    public class VapidTokensStorage : IVapidTokensStorage
    {
        // In a production environment, these would be stored securely in configuration
        public string PublicKey => "BJUGueezdQ1fUnOWNvNGpYtCHQIr2kZpxQHxm0IVD_bW6vzxfplaL7GKcSQYB8EE_JZ_fHjJpuOsGDOBGrMlkG4";

        public string PrivateKey => "6KZ5rCZTlMvYPOGsMbgWV3ID7ZKVzk9tw2fMx_qV2qI";
    }
}
