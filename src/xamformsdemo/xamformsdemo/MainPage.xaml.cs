using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xamformsdemo
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private async void BtnListViewDemo_OnClicked(object sender, EventArgs e)
    {
      await this.Navigation.PushAsync(new ListViewDemoPage());
    }

    private async void BtnCustomControls_OnClicked(object sender, EventArgs e)
    {
      await this.Navigation.PushAsync(new CustomControlsPage());
    }
  }
}
