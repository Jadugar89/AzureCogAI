using System.Threading.Tasks;

namespace AzureCogAI.Services
{
    public interface ITextRecognizerService
    {
        Task<string> GetTextFromImage();
    }
}