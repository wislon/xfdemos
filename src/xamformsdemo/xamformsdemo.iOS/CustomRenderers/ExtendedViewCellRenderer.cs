using System;
using System.ComponentModel;
using UIKit;
using xamformsdemo.CustomControls;
using xamformsdemo.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace xamformsdemo.iOS.CustomRenderers
{
  public class ExtendedViewCellRenderer : ViewCellRenderer
  {

    private ExtendedViewCell _extendedViewCell;
    private UIColor _selectedBackgroundColor;
    private UITableViewCell _cell;
    private UIColor _unselectedBackgroundColor;

    public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
    {
      _extendedViewCell = item as ExtendedViewCell;
      if (_extendedViewCell != null)
      {
        _selectedBackgroundColor = _extendedViewCell.SelectedBackgroundColor.ToUIColor();
        _unselectedBackgroundColor = _extendedViewCell.View.BackgroundColor.ToUIColor();
      }

      if (_extendedViewCell is BindableObject evcAsBindable)
      {
        // wire this up so 'CurrentlySelected' property changes are propagated to the handler below.
        evcAsBindable.PropertyChanged += ExtendedViewCellPropertyChanged;
        _extendedViewCell.BindingContextChanged += ExtendedViewCellOnBindingContextChanged;

      }

      _cell = base.GetCell(item, reusableCell, tv);

      _cell.SelectedBackgroundView = new UIView(_cell.Bounds)
      {
        BackgroundColor = _selectedBackgroundColor
      };

      return _cell;
    }

    private void ExtendedViewCellOnBindingContextChanged(object sender, EventArgs eventArgs)
    {
      this.ExtendedViewCellPropertyChanged(_extendedViewCell, new PropertyChangedEventArgs("CurrentlySelected"));
    }

    private void ExtendedViewCellPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
      if (propertyChangedEventArgs.PropertyName == "CurrentlySelected")
      {
        // this works perfectly for RetainElement. 
        // RecycleElement works too, EXCEPT that when a cell scrolls out of view and back in again, it comes back 
        // selected in white, and the same element one 'screen length' away can be seen to quickly animate as cell 
        // becomes deselected (from being already deselected, it's weird), it animates from black to selected
        // background colour to black again really quickly. If animation wasn't enabled, you'd probably not even see it.
        _cell.SetSelected(_extendedViewCell.CurrentlySelected, true);
        _cell.ContentView.BackgroundColor = _extendedViewCell.CurrentlySelected
          ? _selectedBackgroundColor
          : _unselectedBackgroundColor;
      }

    }

  }
}
