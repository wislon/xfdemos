using System.ComponentModel;

using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using xamformsdemo.CustomControls;
using xamformsdemo.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace xamformsdemo.Droid.CustomRenderers
{
  /// <summary>
  /// Recycle-based views can't store their own 'is selected' properties, or 
  /// they come up as multiple (un)selected items in the list. Unfortunately
  /// you'll have to push that list item's selected status down into its BindingContext,
  /// and pull it out of there when the recycled item is scrolled (back) into view.
  /// It looks like this is the same with RetainElement caching too now; sometimes those
  /// get unpredictably recycled too on different/newer versions of Android. 
  /// So you may as well treat them all as being recycled as well.
  /// </summary>
  public class ExtendedViewCellRenderer : ViewCellRenderer
  {
    private readonly Android.Graphics.Color _defaultBackgroundColor = Android.Graphics.Color.Transparent;

    private Android.Views.View _cellCore;
    private bool _selected;

    /// <summary>
    /// In Recycled views, this will only get called for as many items as are on the screen when the list
    /// is first rendered. After that, it'll never come back here.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="convertView"></param>
    /// <param name="parent"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
    {
      _cellCore = base.GetCellCore(item, convertView, parent, context);
      return _cellCore;
    }

    /// <summary>
    /// Recycled views will NEVER fire the "IsSelected" property-changed, only 
    /// Index and BindingContext. If you're using RetainElement, you'll still 
    /// see the IsSelected property, but it appears to be unreliable as some of the
    /// views still get recycled. You'll get views which you've never 'seen' before
    /// coming up as 'IsSelected', but unpredictably.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
      base.OnCellPropertyChanged(sender, args);

      if (args.PropertyName == "BindingContext")
      {
        if (!(sender is ExtendedViewCell extendedViewCell)) return;
        if (!(extendedViewCell.BindingContext is ISelectableViewModel viewModel)) return;

        _selected = viewModel.IsSelected;

        if (_selected)
        {
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
