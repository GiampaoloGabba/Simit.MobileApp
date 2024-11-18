
using System;
using System.Reflection;
using Prism;
using Xamarin.Forms;

namespace Simit.Views
{
    public partial class ProgrammaPage : IActiveAware
    {
        public bool IsActive { get; set; }
        private MethodInfo _dynMethod = null;

        public event EventHandler IsActiveChanged;
        public ProgrammaPage()
        {
            InitializeComponent();
        }

        private void OnIsActiveChanged()
        {
            //Hack per selezionare programmaticamente tabview
            //https://github.com/xamarin/XamarinCommunityToolkit/issues/595#issuecomment-731575942
            if (IsActive)
            {
                if (_dynMethod == null)
                    _dynMethod = MyTabView.GetType().GetMethod("UpdateSelectedIndex", BindingFlags.NonPublic | BindingFlags.Instance);

                if (MyTabView.SelectedIndex > 0)
                {
                    _dynMethod?.Invoke(MyTabView, new object[] { 0, false });
                }
            }
        }
    }
}
