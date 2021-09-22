
namespace GSmartHR.Services.Security
{
    public interface IEncryptionService
    {
        string GenerateSalt();
        string GetHashPassword(string password, string salt);
    }
}
