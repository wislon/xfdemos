using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using xamformsdemo.CustomControls;
using Xamarin.Forms;

namespace xamformsdemo
{
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

      private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
          // how to locate the cell from SelectedItem and set child-control visibility using behaviours: https://forums.xamarin.com/discussion/comment/269002/#Comment_269002
          // how to get the selected viewcell: https://forums.xamarin.com/discussion/72411/how-to-get-current-item-in-itemtemplate
          // should probably use a 'is selected' property in the viewmodel tho, since viewcells are recycled.
      }
  }

  public class MyModel
  {
    public string ItemName { get; set; }
    public DateTime LastUpdated { get; set; }
  }
}
