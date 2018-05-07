using System;
using Xamarin.Forms;

namespace xamformsdemo.ViewModels
{
  /// <summary>
  /// Inheriting from BindableObject because then we get INPC for free.
  /// </summary>
  public class MyViewModel : BindableObject, ISelectableViewModel
  {
    private string _itemName;
    private DateTime _lastUpdated;
    private bool _isSelected;

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

    public bool IsSelected
    {
      get => _isSelected;
      set
      {
        _isSelected = value;
        OnPropertyChanged(nameof(IsSelected));
      }
    }

  }
}