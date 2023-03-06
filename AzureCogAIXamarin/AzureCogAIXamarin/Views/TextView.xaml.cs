using AzureCogAIXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AzureCogAIXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextView : ContentPage
    {
        public TextView()
        {
            InitializeComponent();
            BindingContext = new TextViewModel();
        }
    }
}