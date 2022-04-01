using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAvaliacaoGinasio.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : MasterDetailPage
    {
        public PaginaPrincipal()
        {
            InitializeComponent();
            btnHome_Clicked(new object(), new EventArgs());
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage( new PageHome());
            IsPresented = false; //esconder o menu
        }

        private void btnSobre_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage( new PageSobre());
            IsPresented = false; //esconder o menu
        }

        private void btnListar_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage( new PageListar());
            IsPresented = false; //esconder o menu
        }

        private void btnRegistar_Clicked(object sender, EventArgs e)
        {

            Detail = new NavigationPage( new PageRegistar());
            IsPresented = false; //esconder o menu
        }
    }
}