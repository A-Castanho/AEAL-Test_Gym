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
    public partial class PageImc : ContentPage
    {
        public PageImc()
        {
            InitializeComponent();
        }

        private void btnCalcular_Clicked(object sender, EventArgs e)
        {
            try 
            {
                float peso = float.Parse(entPeso.Text);
                float altura = float.Parse(entAltura.Text);

                var imc = peso / (altura*altura);
                DisplayAlert("IMC","O seu IMC é de:\n"+imc,"Ok");
            }
            catch 
            {
                DisplayAlert("Erro ao calcular o IMC","Insira dados válidos","Ok");
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}