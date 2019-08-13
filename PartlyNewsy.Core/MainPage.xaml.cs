using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartlyNewsy.Core;
using PartlyNewsy.Models;
using Xamarin.Forms;

namespace PartlyNewsy
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void GetData(object sender, EventArgs e)
        {
            var newsService = new NewsService();

            var article = await newsService.GetTopNews();

            theLabel.Text = article.Headline;
        }
    }
}
