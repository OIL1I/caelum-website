using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using ChallengeViewer.Models;

namespace ChallengeViewer.View;

public partial class Start : Window
{
    public Start()
    {
        InitializeComponent();
        LblVersionNr.Content = $"v{Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version}";
    }

    private async void BtnConnect_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.TxtboxSharecode.Text != "")
        {
            //TODO: Change local url to prod url
            var winchlng =
                await new HttpClient().GetFromJsonAsync<Winchallenge>(
                    $"http://13.49.3.121/api/GetChallenge/{this.TxtboxSharecode.Text}");
            // var winchlng = JsonSerializer.Deserialize<Winchallenge>(new FileStream("Test.json", FileMode.Open));
            if (winchlng != null)
            {
                this.LblNoChallengeFound.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Hidden;
                (new ChallengeView(winchlng)).ShowDialog();
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.LblNoChallengeFound.Visibility = Visibility.Visible;
            }
        }
        else
        {
            this.LblNoChallengeFound.Visibility = Visibility.Visible;
        }
    }
}