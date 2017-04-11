using System.ComponentModel;

using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using xamformsdemo.CustomControls;
using xamformsdemo.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace xamformsdemo.Droid.CustomRenderers
{
  public class ExtendedViewCellRenderer : ViewCellRenderer
  {

    private Android.Views.View _cellCore;
    private Drawable _unselectedBackground;
    private bool _selected;

    protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
    {
      _cellCore = base.GetCellCore(item, convertView, parent, context);

      // Save original background to roll-back to it when not selected,
      // we're assuming that no cells will be selected on creation.
      _selected = false;
      _unselectedBackground = _cellCore.Background;

      return _cellCore;
    }

    protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
      base.OnCellPropertyChanged(sender, args);

      if (args.PropertyName == "IsSelected")
      {
        // Had to create a property to track the selection because cellCore.Selected is always false.
        _selected = !_selected;

        if (_selected)
        {
          var extendedViewCell = sender as ExtendedViewCell;
          _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
        }
        else
        {
          _cellCore.SetBackground(_unselectedBackground);
        }
      }
    }
  }

}
