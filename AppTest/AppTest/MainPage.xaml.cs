using Plugin.LatestVersion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(async () =>
            {
                Loader.IsVisible = true;
                await Task.Delay(250);
                Lbl_LocalVersion.Text = CrossLatestVersion.Current.InstalledVersionNumber;
                Lbl_StoreVersion.Text = await CrossLatestVersion.Current.GetLatestVersionNumber();
                if (await CrossLatestVersion.Current.IsUsingLatestVersion())
                    Lbl_IsLastVersion.Text = "True";
                else
                    Lbl_IsLastVersion.Text = "False";
                Loader.IsVisible = false;
                await Task.Delay(250);
            });
            Btn_Open.Clicked += (s, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Loader.IsVisible = true;
                    await Task.Delay(250);
                    await CrossLatestVersion.Current.OpenAppInStore();
                    Loader.IsVisible = false;
                    await Task.Delay(250);
                });
            };
        }
    }
}
