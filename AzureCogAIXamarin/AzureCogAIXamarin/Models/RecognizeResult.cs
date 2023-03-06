using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AzureCogAIXamarin.Models
{
    public class RecognizeResult
    {
        public ImageSource ImageSource { get; set; }
        public string TextFromAI { get; set; } 
    }
}
