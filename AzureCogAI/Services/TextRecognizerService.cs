
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
using System.Windows.Controls;
using System.Windows.Media;

namespace AzureCogAI.Services
{
    public class TextRecognizerService : ITextRecognizerService
    {
        // Add your Computer Vision subscription key and endpoint
        private string subscriptionKey = "subscriptionKey";
        private string endpoint = "endpoint";
        private readonly TextRecognizer textRecognizer;

        public TextRecognizerService()
        {
             textRecognizer = new TextRecognizer(subscriptionKey, endpoint);
        }

        public async Task<string> GetTextFromImage()
        {
            Debug.WriteLine("Azure Cognitive Services Computer Vision - .NET quickstart example");
            

            var imagePath = OpenDialog();
            if (!String.IsNullOrEmpty(imagePath))
            {
                return await textRecognizer.ReadImage(imagePath);
            }
            return string.Empty;

        }

        private string OpenDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = ""; // Default file name
            dialog.DefaultExt = ".jpg"; // Default file extension
            dialog.Filter = "XML documents (.jpg)|*.jpg"; // Filter files by extension

            // Show open file dialog box
            bool? name = dialog.ShowDialog();
            // Process open file dialog box results
            if (name == true)
            {
                return dialog.FileName;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
