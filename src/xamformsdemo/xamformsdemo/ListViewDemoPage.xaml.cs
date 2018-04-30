using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamformsdemo
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ListViewDemoPage : ContentPage
  {

    public ObservableCollection<MyModel> MyModelsList { get; set; }

    public ListViewDemoPage()
    {
      InitializeComponent();


      MyModelsList = new ObservableCollection<MyModel>(MakeAListOfMyModels());

      BindingContext = MyModelsList;
    }

    private List<MyModel> MakeAListOfMyModels()
    {
      var lst = new List<MyModel>();
      for (int i = 0; i < 20; i++)
      {
        lst.Add(new MyModel() {ItemName = $"This is Item {i:00}", LastUpdated = DateTime.Now.AddMinutes(-1 * i)});
      }
      return lst;
    }
  }

  public class MyModel
  {
    public string ItemName { get; set; }
    public DateTime LastUpdated { get; set; }
  }
}
