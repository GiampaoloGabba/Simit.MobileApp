using Android.Content;
using Simit.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RadioButton), typeof(PatchedRadioButtonRenderer))]
namespace Simit.Droid.Renderers
{
    /// <summary>
    /// A workaround for the issue in the in-box renderer.
    /// https://github.com/xamarin/Xamarin.Forms/issues/11700
    /// </summary>
    public class PatchedRadioButtonRenderer : RadioButtonRenderer
    {
        public PatchedRadioButtonRenderer(Context context) : base(context) {}

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            // Apply text to native radio button
            Control.Text = e.NewElement.Text;

            base.OnElementChanged(e);
        }
    }
}
