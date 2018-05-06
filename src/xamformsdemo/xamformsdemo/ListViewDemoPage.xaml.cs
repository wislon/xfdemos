using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace xamformsdemo
{
  public partial class ListViewDemoPage : ContentPage
  {
    private MyViewModel _currentViewModel;

    public ObservableCollection<MyViewModel> MyModelsList { get; set; }

    public ListViewDemoPage()
    {
      InitializeComponent();

      MyModelsList = new ObservableCollection<MyViewModel>(MakeAListOfMyModels());

      BindingContext = MyModelsList;
    }

    private List<MyViewModel> MakeAListOfMyModels()
    {
      var lst = new List<MyViewModel>();
      for (int i = 0; i < 20; i++)
      {
        lst.Add(new MyViewModel() {ItemName = $"This is Item {i:00}", LastUpdated = DateTime.Now.AddMinutes(-1 * i)});
      }
      return lst;
    }

      private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
        // need to track the previously selected viewmodel or we can't 'deselect' it
        if (e.SelectedItem == null)
        {
          if (_currentViewModel != null)
          {
            _currentViewModel.IsSelected = false;
          }
        }
        else
        {
          if (_currentViewModel != null)
          {
            _currentViewModel.IsSelected = false;
          }
          _currentViewModel = (MyViewModel)e.SelectedItem;
          _currentViewModel.IsSelected = true;
        }
        // how to locate the cell from SelectedItem and set child-control visibility using behaviours: https://forums.xamarin.com/discussion/comment/269002/#Comment_269002
        // how to get the selected viewcell: https://forums.xamarin.com/discussion/72411/how-to-get-current-item-in-itemtemplate
      }
  }

  public class MyViewModel : INotifyPropertyChanged, ISelectableViewModel
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private string _itemName;
    private DateTime _lastUpdated;

    public string ItemName
    {
      get => _itemName;
      set
      {
        _itemName = value;
        OnPropertyChanged(nameof(ItemName));
      }
    }

    public DateTime LastUpdated
    {
      get => _lastUpdated;
      set
      {
        _lastUpdated = value;
        OnPropertyChanged(nameof(LastUpdated));
      }
    }

    /// <summary>
    /// Do not bind to this
    /// </summary>
    public bool IsSelected { get; set; }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
