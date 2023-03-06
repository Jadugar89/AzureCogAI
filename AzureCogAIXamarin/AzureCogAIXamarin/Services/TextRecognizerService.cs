using AzureCogAIXamarin.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;


namespace AzureCogAIXamarin.Services
{
    public class TextRecognizerService : ITextRecognizerService
    {
        // Add your Computer Vision subscription key and endpoint
        private string subscriptionKey = "subscriptionKey";
        private string endpoint = "endpoint";
        private ComputerVisionClient client;


        public TextRecognizerService()
        {
            client = Authenticate(endpoint, subscriptionKey);
        }

        public async Task<RecognizeResult> GetTextFromImage()
        {
            Debug.WriteLine("Azure Cognitive Services Computer Vision - .NET quickstart example");


            var imageSteam = await TakePhotoAsync();
            if (imageSteam != null)
            {
                return await ReadImage(imageSteam);
            }
            return null;

        }
        private async Task<MediaFile> TakePhotoAsync()
        {

            try
            {
                

                return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = CameraDevice.Rear,
                    RotateImage = true,
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                return null;
            }
        }

        private async Task<RecognizeResult> ReadImage(MediaFile image)
        {
            Debug.WriteLine("----------------------------------------------------------");
            Debug.WriteLine("READ FILE FROM URL");
            
       
            try
            {

                var sb = new StringBuilder();
                using (var imageStream =  image.GetStream())
                {
                    var textHeaders = await client.RecognizePrintedTextInStreamAsync(true, imageStream, OcrLanguages.En);

                    if (textHeaders.Regions.Count > 0)
                    {
                        foreach (var region in textHeaders.Regions.SelectMany(x => x.Lines))
                        {
                            var words = region.Words.Select(x => x.Text).ToArray();
                            if (words.Any())
                            {
                                sb.AppendLine(String.Join(" ", words));
                            }
                        }
                    }
                }
                    var steam =  image.GetStream();

                    var imageSource =  ImageSource.FromStream(() => steam);
    
                    return new RecognizeResult()
                    {
                        ImageSource = imageSource,
                        TextFromAI = sb.ToString(),
                    };
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        private ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }
    }
}
