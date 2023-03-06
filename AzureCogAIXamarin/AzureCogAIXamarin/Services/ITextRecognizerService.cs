using AzureCogAIXamarin.Models;
using System.Threading.Tasks;

namespace AzureCogAIXamarin.Services
{
    public interface ITextRecognizerService
    {
        Task<RecognizeResult> GetTextFromImage();
    }
}