using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AzureCogAI
{
    public class TextRecognizer 
    {
        private readonly string subscriptionKey;
        private readonly string endpoint;
        private ComputerVisionClient client;

        public TextRecognizer(string subscriptionKey, string endpoint)
        {
            this.subscriptionKey = subscriptionKey;
            this.endpoint = endpoint;
            this.client = Authenticate(endpoint, subscriptionKey);
        }

        private ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        public async Task<string> ReadImage(string urlFile)
        {
            Debug.WriteLine("----------------------------------------------------------");
            Debug.WriteLine("READ FILE FROM URL");


            using (var imageStream = File.OpenRead(urlFile))
            {
                var textHeaders = await client.RecognizePrintedTextInStreamAsync(true, imageStream, OcrLanguages.En);
                var sb = new StringBuilder();
                if (textHeaders.Regions.Count > 0)
                {
                    foreach (var region in textHeaders.Regions.SelectMany(x=>x.Lines))
                    {
                        var words = region.Words.Select(x => x.Text).ToArray();
                        if (words.Any())
                        {
                            sb.AppendLine(String.Join(" ", words));
                        }
                    }
                }
                return sb.ToString();
            }
        } 
    }
}
