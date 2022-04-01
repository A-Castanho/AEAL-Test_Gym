using AppAvaliacaoGinasio.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAvaliacaoGinasio
{
    public partial class App : Application
    {
        public static string DbPath { get; internal set; }
        public static string DbName { get; internal set; }

        public App()
        {
            InitializeComponent();

            MainPage = new PaginaPrincipal();
        }
        public App(string dbPath, string dbName)
        {
            InitializeComponent();
            MainPage = new PaginaPrincipal();
            DbName = dbName;
            DbPath = dbPath;
            MainPage = new PaginaPrincipal();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
