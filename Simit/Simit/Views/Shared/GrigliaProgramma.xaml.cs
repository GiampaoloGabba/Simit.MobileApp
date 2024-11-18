using System.Collections;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Simit.Views.Shared
{

    public partial class GrigliaProgramma: Grid
    {
        public static readonly BindableProperty ProgrammaProperty = BindableProperty.Create(nameof (Programma), typeof (IList), typeof (TabView), propertyChanged: OnProgrammaChanged);
        private static void OnProgrammaChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable != null && newvalue != null)
            {
                var control = (GrigliaProgramma) bindable;
                control.Collection.ItemsSource = (IList)newvalue;
            }
        }

        public IList Programma
        {
            get => (IList) GetValue(ProgrammaProperty);
            set => SetValue(ProgrammaProperty, (object) value);
        }

        public GrigliaProgramma()
        {
            InitializeComponent();
        }
    }
}
