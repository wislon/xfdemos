using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Widget;
using xamformsdemo.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using Switch = Android.Widget.Switch;

using ChangeColorSwitchEffect = xamformsdemo.Droid.Effects.ChangeColorSwitchEffect;

[assembly: ExportEffect(typeof(ChangeColorSwitchEffect), "ChangeColorSwitchEffect")]

namespace xamformsdemo.Droid.Effects
{
  /// <summary>
  /// Based on the samples at https://github.com/FormsCommunityToolkit/Effects
  /// and  http://stackoverflow.com/questions/11253512/change-on-color-of-a-switch
  /// </summary>
  [Android.Runtime.Preserve(AllMembers = true)]
  public class ChangeColorSwitchEffect : PlatformEffect
  {
    private Color _trueColor;
    private Color _falseColor;

    // slightly darker for the tracks, otherwise there's no virtual
    // 'depth' below the 'thumb' part of the slider.
    private Color _falseColorDarker;
    private Color _trueColorDarker;

    protected override void OnAttached()
    {
      if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
      {
        _trueColor = (Color)Element.GetValue(ChangeColorEffect.TrueColorProperty);
        _trueColorDarker = _trueColor.AddLuminosity(-0.25);

        _falseColor = (Color)Element.GetValue(ChangeColorEffect.FalseColorProperty);
        _falseColorDarker = _falseColor.AddLuminosity(-0.25);

        ((SwitchCompat)Control).CheckedChange += OnCheckedChange;

        ((SwitchCompat)Control).ThumbDrawable.SetColorFilter(_falseColor.ToAndroid(), PorterDuff.Mode.Multiply);
        ((SwitchCompat)Control).TrackDrawable.SetColorFilter(_falseColorDarker.ToAndroid(), PorterDuff.Mode.Multiply);
      }
    }

    private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
    {
      if (checkedChangeEventArgs.IsChecked)
      {
        ((SwitchCompat)Control).ThumbDrawable.SetColorFilter(_trueColor.ToAndroid(), PorterDuff.Mode.Multiply);
        ((SwitchCompat)Control).TrackDrawable.SetColorFilter(_trueColorDarker.ToAndroid(), PorterDuff.Mode.Multiply);
      }
      else
      {
        ((SwitchCompat)Control).ThumbDrawable.SetColorFilter(_falseColor.ToAndroid(), PorterDuff.Mode.Multiply);
        ((SwitchCompat)Control).TrackDrawable.SetColorFilter(_falseColorDarker.ToAndroid(), PorterDuff.Mode.Multiply);
      }
    }

    protected override void OnDetached()
    {
      if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
      {
        ((Switch)Control).CheckedChange -= OnCheckedChange;
      }
    }
  }
}