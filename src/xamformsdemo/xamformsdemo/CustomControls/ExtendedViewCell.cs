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
    /// The IsSelected property.
    /// </summary>
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create("IsSelected", typeof(bool), typeof(ExtendedViewCell), false);

    /// <summary>
    /// Gets or sets the SelectedBackgroundColor.
    /// </summary>
    public Color SelectedBackgroundColor
    {
      get { return (Color)GetValue(SelectedBackgroundColorProperty); }
      set { SetValue(SelectedBackgroundColorProperty, value); }
    }

    public bool IsSelected
    {
      get { return (bool)GetValue(IsSelectedProperty); }
      set { SetValue(IsSelectedProperty, value); }
    }
  }
}