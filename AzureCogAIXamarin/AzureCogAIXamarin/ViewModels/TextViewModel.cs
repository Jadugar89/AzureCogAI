using AzureCogAIXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AzureCogAIXamarin.ViewModels
{
    public class TextViewModel : BaseViewModel
    {
        private readonly ITextRecognizerService textRecognizerService;
        public Command SendImageToAI { get; }
        private string textFromAi;

        public string TextFromAI
        {
            get { return textFromAi; }
            set { textFromAi = value;
                OnPropertyChanged(nameof(TextFromAI));
            }
        }
        private ImageSource imageFromAI;

        public ImageSource ImageFromAI
        {
            get { return imageFromAI; }
            set { imageFromAI = value;
                OnPropertyChanged(nameof(ImageFromAI));
            }
        }


        public TextViewModel()
        {
            SendImageToAI = new Command(OnSendImageToAI);
            textRecognizerService =  DependencyService.Get<ITextRecognizerService>();
        }

        private async void OnSendImageToAI()
        {
            
            var result= await textRecognizerService.GetTextFromImage();
            if (result != null)
            {
                if (String.IsNullOrEmpty(result.TextFromAI))
                {
                    ImageFromAI = null;
                    await Application.Current.MainPage.DisplayAlert("Text Form AI", "No text can be recognized on the photo", "ok");
                }
                else
                {
                    TextFromAI = result.TextFromAI;
                    ImageFromAI = result.ImageSource;

                }
            }

            
        }
    }
}
