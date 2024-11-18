using Xamarin.Forms;

namespace Simit.Views.Shared
{
    public class NotchContentPage : ContentPage
    {
        public static readonly BindableProperty ReActivateEdgeSwipeOniOSProperty =
            BindableProperty.Create(nameof(ReActivateEdgeSwipeOniOS), typeof(bool), typeof(ContentPage), false);

        public static readonly BindableProperty SafeAreaTopProperty =
            BindableProperty.Create(nameof(SafeAreaTop), typeof(double), typeof(ContentPage), 0.00);

        public static readonly BindableProperty SafeAreaBottomProperty =
            BindableProperty.Create(nameof(SafeAreaBottom), typeof(double), typeof(ContentPage), 0.00);

        public bool ReActivateEdgeSwipeOniOS
        {
            get => (bool) GetValue(ReActivateEdgeSwipeOniOSProperty);
            set => SetValue(ReActivateEdgeSwipeOniOSProperty, value);
        }

        public double SafeAreaTop
        {
            get => (double) GetValue(SafeAreaTopProperty);
            set => SetValue(SafeAreaTopProperty, value);
        }

        public double SafeAreaBottom
        {
            get => (double) GetValue(SafeAreaBottomProperty);
            set => SetValue(SafeAreaBottomProperty, value);
        }
    }
}