
using AppAvaliacaoGinasio.Models;
using AppAvaliacaoGinasio.Services;
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
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            datePicker.IsVisible = false;
            AtualizarLista();
        }
        public void AtualizarLista()
        {
            ServiceDbTreinos dbNotas = new ServiceDbTreinos(App.DbPath);
            if (datePicker.IsVisible)
            {
                ListaTreinos.ItemsSource = dbNotas
                    .Procurar(datePicker.Date);
            }
            else
                ListaTreinos.ItemsSource = dbNotas.GetTreinos();
        }
        private void swPreferido_Toggled(object sender, ToggledEventArgs e)
        {
            AtualizarLista();
        }


        private void switchFiltro_Toggled(object sender, ToggledEventArgs e)
        {
            datePicker.IsVisible = e.Value;
        }

        private void ListaTreinos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Treino treino = (Treino)e.SelectedItem;
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new PageRegistar(treino)));
        }

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            AtualizarLista();
        }
    }
}