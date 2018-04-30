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
    private readonly Android.Graphics.Color _defaultBackgroundColor = Android.Graphics.Color.Transparent;

    private Android.Views.View _cellCore;
    private Drawable _unselectedBackground;
    private bool _selected;

    protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
    {
      var extendedViewCell = (ExtendedViewCell)item;
     
      _cellCore = base.GetCellCore(item, convertView, parent, context);

      TODO  remember to unsubscribe from this
      _cellCore.Click += (sender, args) =>
      {
        TODO do we really need to track the selected status in the xplat view??
        extendedViewCell.IsSelected = !extendedViewCell.IsSelected;
        _selected = extendedViewCell.IsSelected;
      };

      // Save/set original background to roll-back to it when not selected,
      // we're assuming that no cells will be selected on creation.
      //_selected = extendedViewCell.IsSelected;
      _unselectedBackground = _cellCore.Background ?? new ColorDrawable(_defaultBackgroundColor);


      return _cellCore;
    }

    /// <summary>
    /// TODO figure out the ways to handle recycleelement/retainelement etc, this doesn't always
    /// fire 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
      base.OnCellPropertyChanged(sender, args);

      which IsSelected is showing, the one for the xamforms view, or the android native one?
      if (args.PropertyName == "IsSelected" || args.PropertyName=="Index")
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
          _cellCore.SetBackgroundColor(_defaultBackgroundColor);
        }
      }
    }
  }

}
