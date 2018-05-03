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
  /// <summary>
  /// TODO it looks like we need to have a different renderer for Recycle-based views. Since the native views
  /// TODO are recycled, they can't track their own 'selected' properties (or you'll see more than one cell becoming
  /// TODO selected as you scroll back and forth). We'll need the XF 'ExtendedViewCell' to keep track of its
  /// TODO own 'selected' state (via ItemSelected event?) and then feed that into here, via GetCellCore and 
  /// TODO probably the BindingContextChanged property for OnCellPropertyChanged below (via 'sender').
  /// TODO Ppl wonder why Xamarin hasn't done this for us... I think  this explains it, it's too %$#%$ hard to do, because it's 
  /// TODO bespoke for every custom view for every list for every app.
  /// TODO Next steps: 
  /// TODO 0. Revert Everything here (to where we branched) and check that the RetainElement still works as expected.
  /// TODO 1. Create a different ExtendedRecycledViewCellRenderer (or something), specifically for the Recycle-type implementation.
  /// TODO 2. Add another demo button to showcase Recycle-type implementation
  /// </summary>
  public class ExtendedViewCellRenderer : ViewCellRenderer, INativeElementView
  {
    private readonly Android.Graphics.Color _defaultBackgroundColor = Android.Graphics.Color.Transparent;

    private Android.Views.View _cellCore;
    private Drawable _unselectedBackground;
    private bool _selected;
    public Element Element { get; private set; }

    protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
    {
      Element = (ExtendedViewCell)item;
     
      _cellCore = base.GetCellCore(item, convertView, parent, context);

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

      if (args.PropertyName == "BindingContext")
      {
        // Had to create a property to track the selection because cellCore.Selected is always false.
        if (!(sender is ExtendedViewCell extendedViewCell)) return;
        
        _selected = extendedViewCell.IsSelected;

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
