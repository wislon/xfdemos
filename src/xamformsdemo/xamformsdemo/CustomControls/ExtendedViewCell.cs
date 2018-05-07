using Xamarin.Forms;

namespace xamformsdemo.CustomControls
{
  /// <summary>
  /// Will be swapped out by a Custom Renderer which will override/reset 
  /// the background colour of the cell based on whether it's (un)selected
  /// </summary>
  public class ExtendedViewCell : ViewCell
  {
    /// <summary>
    /// The SelectedBackgroundColor property.
    /// </summary>
    public static readonly BindableProperty SelectedBackgroundColorProperty =
        BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(ExtendedViewCell), Color.Transparent);

    /// <summary>
    /// Gets or sets the SelectedBackgroundColor.
    /// </summary>
    public Color SelectedBackgroundColor
    {
      get { return (Color)GetValue(SelectedBackgroundColorProperty); }
      set { SetValue(SelectedBackgroundColorProperty, value); }
    }

    /// <summary>
    /// I deliberately picked this property name so there would be no
    /// confusion with the 'IsSelected' property that (sometimes)
    /// fires in the custom renderer when you're using RetainElement
    /// </summary>
    public static readonly BindableProperty CurrentlySelectedProperty =
        BindableProperty.Create("CurrentlySelected", typeof(bool), typeof(ExtendedViewCell), false);

    /// <summary>
    /// Gets or sets the IsSelected property.
    /// </summary>
    public bool CurrentlySelected
    {
      get { return (bool)GetValue(CurrentlySelectedProperty); }
      set { SetValue(CurrentlySelectedProperty, value); }
    }

  }
}