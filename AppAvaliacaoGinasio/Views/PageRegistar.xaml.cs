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
    public partial class PageRegistar : ContentPage
    {
        private readonly Treino treinoOriginal = new Treino();
        public PageRegistar(Treino treino)
        {
            InitializeComponent();
            IniciarPickerIntensidade();
            treinoOriginal = treino;
            btnGuardar.Text = "Alterar";
            btnEliminar.IsVisible = true;
            this.pickerIntensidade.SelectedItem = treino.Intensidade.ToString();
            this.datePicker.Date = treino.DateTimeEntrada.Date;
            this.timePickerEntrada.Time = treino.DateTimeEntrada.TimeOfDay;
            this.timePickerSaida.Time = treino.DateTimeSaida.TimeOfDay;
            this.entTipo.Text = treino.TipoTreino;
            this.entCalorias.Text = treino.CaloriasPerdidas.ToString();
            this.entImc.Text = treino.Imc.ToString();
            this.edtEquipamentosUsados.Text = treino.EquipamentosUsados;
            this.edtPartesCorpo.Text = treino.PartesCorpoTrabalhadas;
        }
        public PageRegistar()
        {
            InitializeComponent();
            IniciarPickerIntensidade();
            this.pickerIntensidade.SelectedItem = Treino.IntensidadeFisica.Baixa.ToString();
            this.datePicker.Date = DateTime.Now.Date;
            this.timePickerEntrada.Time = DateTime.Now.TimeOfDay;
        }

        private void IniciarPickerIntensidade()
        {
            pickerIntensidade.ItemsSource = new List<string>
            {
                Treino.IntensidadeFisica.Alta.ToString(),
                Treino.IntensidadeFisica.Media.ToString(),
                Treino.IntensidadeFisica.Baixa.ToString()
            };
        }


        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Treino treino = treinoOriginal;
                treino.Intensidade = (Treino.IntensidadeFisica)Enum.Parse(typeof(Treino.IntensidadeFisica), pickerIntensidade.SelectedItem.ToString());
                treino.DateTimeEntrada = new DateTime
                    (
                        this.datePicker.Date.Year,
                        datePicker.Date.Month,
                        datePicker.Date.Day,
                        timePickerEntrada.Time.Hours,
                        timePickerEntrada.Time.Minutes,
                        timePickerEntrada.Time.Seconds
                    );
                treino.DateTimeSaida = new DateTime
                    (
                        this.datePicker.Date.Year,
                        datePicker.Date.Month,
                        datePicker.Date.Day,
                        timePickerSaida.Time.Hours,
                        timePickerSaida.Time.Minutes,
                        timePickerSaida.Time.Seconds
                    );

                treino.TipoTreino = this.entTipo.Text;
                int.TryParse(this.entCalorias.Text, out int calorias);
                treino.CaloriasPerdidas = calorias;
                double.TryParse(this.entImc.Text, out double imc);
                treino.Imc = imc;
                treino.EquipamentosUsados = this.edtEquipamentosUsados.Text;
                treino.PartesCorpoTrabalhadas = this.edtPartesCorpo.Text;
                ServiceDbTreinos dbTreinos = new ServiceDbTreinos(App.DbPath);
                if (btnGuardar.Text == "Inserir")
                {
                    dbTreinos.Inserir(treino);
                    DisplayAlert("Resultado", dbTreinos.StatusMessage, "Ok");
                }
                else
                {
                    dbTreinos.Atualizar(treino);
                    DisplayAlert("Resultado", dbTreinos.StatusMessage, "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message,"ok");
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            try { Application.Current.MainPage.Navigation.PopModalAsync(); } catch { }
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageListar());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var confirmacao = await DisplayAlert("Eliminar anotação", "Deseja eliminar a notação?", "Sim", "Não");
            if (confirmacao) 
            {
                ServiceDbTreinos dbNotas = new ServiceDbTreinos(App.DbPath);
                dbNotas.Apagar(treinoOriginal.Id);
                DisplayAlert("Resultado", dbNotas.StatusMessage, "Ok");
                ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage( new PageListar());
            }
        }

        private async void btnIMC_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new PageImc()));
        }
    }
}