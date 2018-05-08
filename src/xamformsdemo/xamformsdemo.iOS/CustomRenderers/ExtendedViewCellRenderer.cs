using System;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using xamformsdemo.CustomControls;
using xamformsdemo.iOS.CustomRenderers;
using xamformsdemo.ViewModels;
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

    public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
    {
      _extendedViewCell = item as ExtendedViewCell;
      _selectedBackgroundColor = _extendedViewCell.SelectedBackgroundColor.ToUIColor();

      if (_extendedViewCell is BindableObject evcAsBindable)
      {
        // wire this up so 'CurrentlySelected' property changes are propagated to the handler below.
        evcAsBindable.PropertyChanged += ExtendedViewCellPropertyChanged;
      }

      _cell = base.GetCell(item, reusableCell, tv);

      _cell.SelectedBackgroundView = new UIView(_cell.Bounds)
      {
        BackgroundColor = _selectedBackgroundColor
      };

      return _cell;
    }

    private void ExtendedViewCellPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
      if (propertyChangedEventArgs.PropertyName == "CurrentlySelected")
      {
        // this works for RetainElement, but not for RecycleElement (need another propertyname ?)
        // animation shows what's happening, as cell becomes deselected, it animates from selected background colour to black,
        // and selected cell becomes white again
        _cell.SetSelected(_extendedViewCell.CurrentlySelected, true);
      }
    }

  }
}
