using AzureCogAI.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AzureCogAI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ITextRecognizerService textRecognizerService;

        public IRelayCommand BtnSendToAi { get; set; }
        private string textFromImage;

        public string TextFromImage
        {
            get { return textFromImage; }
            set { textFromImage = value;
                OnPropertyChanged(nameof(TextFromImage)); 
            }
        }


        public MainViewModel(ITextRecognizerService textRecognizerService)
        {
            BtnSendToAi = new RelayCommand(SendToAi);
            this.textRecognizerService = textRecognizerService;
        }

        private async void SendToAi()
        {
           TextFromImage = await textRecognizerService.GetTextFromImage();
            if (String.IsNullOrEmpty(TextFromImage))
            {
                MessageBox.Show("Not recognized!");
            }
        }
    }
}
