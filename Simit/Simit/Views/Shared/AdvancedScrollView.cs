using Xamarin.Forms;

namespace Simit.Views.Shared
{
    public partial class AdvancedScrollView : ScrollView
    {
        public static readonly BindableProperty ShouldRecognizeSimultaneouslyProperty = BindableProperty.Create(nameof(ShouldRecognizeSimultaneously), typeof(bool), typeof(ScrollView), false);
        public static readonly BindableProperty DisableContentInsetAdjustmentProperty = BindableProperty.Create(nameof(DisableContentInsetAdjustment), typeof(bool), typeof(ScrollView), true);

        public bool ShouldRecognizeSimultaneously
        {
            get => (bool)GetValue(ShouldRecognizeSimultaneouslyProperty);
            set => SetValue(ShouldRecognizeSimultaneouslyProperty, value);
        }

        public bool DisableContentInsetAdjustment
        {
            get => (bool)GetValue(DisableContentInsetAdjustmentProperty);
            set => SetValue(DisableContentInsetAdjustmentProperty, value);
        }

        public AdvancedScrollView()
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Never;
        }
    }
}
