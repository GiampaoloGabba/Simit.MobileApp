using Xamarin.Forms;

namespace Simit.Core.Effects
{
    public class SafeAreaPaddingEffect : RoutingEffect
    {
        public int MinPaddingTop { get; set; } = 0;

        public SafeAreaPaddingEffect() : base("Simit.SafeAreaPadding")
        {
        }
    }
}