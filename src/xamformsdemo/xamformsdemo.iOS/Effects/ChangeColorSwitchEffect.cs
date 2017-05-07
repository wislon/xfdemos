using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using ChangeColorSwitchEffect = xamformsdemo.iOS.Effects.ChangeColorSwitchEffect;
using XECCSwitchEffect = xamformsdemo.Effects.ChangeColorSwitchEffect;

[assembly: ExportEffect(typeof(ChangeColorSwitchEffect), "ChangeColorSwitchEffect")]

namespace xamformsdemo.iOS.Effects
{
  public class ChangeColorSwitchEffect :PlatformEffect
  {
    private Color _trueColor;
    private Color _falseColor;
    private Color _thumbColor;

    protected override void OnAttached()
    {
      _trueColor = (Color)Element.GetValue(XECCSwitchEffect.TrueColorProperty);
      _falseColor = (Color)Element.GetValue(XECCSwitchEffect.FalseColorProperty);
      _thumbColor = (Color)Element.GetValue(XECCSwitchEffect.ThumbColorProperty);

      var switchControl = Control as UISwitch;
      if (switchControl != null)
      {
        switchControl.TintColor = UIColor.FromRGBA((nfloat)_falseColor.R, (nfloat)_falseColor.G, (nfloat)_falseColor.B, 0.75f);
        // enable and fiddle with these to change initial track colour 
        // switchControl.Layer.CornerRadius = switchControl.Frame.Height / 2;
        // switchControl.BackgroundColor = switchControl.TintColor;

        switchControl.OnTintColor = UIColor.FromRGBA((nfloat)_trueColor.R, (nfloat)_trueColor.G, (nfloat)_trueColor.B, 0.75f);
        switchControl.ThumbTintColor = UIColor.FromRGBA((nfloat)_thumbColor.R, (nfloat)_thumbColor.G, (nfloat)_thumbColor.B, 1.0f);
      }
    }

    protected override void OnDetached()
    {

    }
  }
}