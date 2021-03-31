using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecetteMaster.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;

namespace RecetteMaster
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RecetteEntryPage), typeof(RecetteEntryPage));
            
        }
    }
}