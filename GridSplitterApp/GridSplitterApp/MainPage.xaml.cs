using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GridSplitterApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

			Image.Source = ImageSource.FromResource("GridSplitterApp.baboon.png");
        }
    }
}
