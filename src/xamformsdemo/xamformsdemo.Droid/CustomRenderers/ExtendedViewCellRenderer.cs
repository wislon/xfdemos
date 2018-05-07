using System.ComponentModel;

using Android.Content;
using Android.Views;
using Color = Android.Graphics.Color;

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
  /// get unpredictably recycled on different/newer versions of Android. 
  /// So you may as well treat them all as being recycled as well.
  /// </summary>
  public class ExtendedViewCellRenderer : ViewCellRenderer
  {
    private readonly Android.Graphics.Color _defaultBackgroundColor = Android.Graphics.Color.Transparent;

    private Android.Views.View _cellCore;
    private ExtendedViewCell _extendedViewCell;
    private bool _selected;
    private Color _selectedBackgroundColor;

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

      _extendedViewCell = item as ExtendedViewCell;

      if (_extendedViewCell != null && _extendedViewCell is BindableObject evcAsBindable)
      {
        _selectedBackgroundColor = _extendedViewCell.SelectedBackgroundColor.ToAndroid();
        // wire this up so 'CurrentlySelected' property changes are propagated to the handler below.
        evcAsBindable.PropertyChanged += this.OnCellPropertyChanged;
      }

      return _cellCore;
    }

    /// <summary>
    /// Recycled views will NEVER fire the "IsSelected" property-changed for views, only 
    /// 'Index' and 'BindingContext'.
    /// If you're using RetainElement, you'll still see the IsSelected property, but it 
    /// appears to be unprectable reliable as some of the views still appear to get recycled. 
    /// You'll get views which you've never 'seen' before coming up as 'IsSelected'.
    /// 
    /// We can access the 'CurrentlySelected' property on the ExtendedViewCell because 
    /// we've wired up the PropertyChanged event on the EVC (as a bindable object) to this
    /// class's OnCellPropertyChanged event, above.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
      base.OnCellPropertyChanged(sender, args);

      if (!(sender is ExtendedViewCell extendedViewCell)) return;

      if (args.PropertyName == "CurrentlySelected" || args.PropertyName == "BindingContext")
      {
        _selected = extendedViewCell.CurrentlySelected;

        // collapse this to ternary if you want, this is just for easier debugging
        if (_selected)
        {
          _cellCore.SetBackgroundColor(_selectedBackgroundColor);
        }
        else
        {
          _cellCore.SetBackgroundColor(_defaultBackgroundColor);
        }
      }
    }

  }
}
