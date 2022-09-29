using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Horus.Services;
using Horus.Models;
using Horus.Views;

namespace Horus.ViewModels
{
    public class LoginPageVM:BaseViewModel
    {
       
        #region Variables
        string txtSample;
        private bool _IsPassword = true;
        string txtEmail;
        string txtContraseña;
        ApiService apiService;

        #endregion

        #region CONSTRUCTOR
        public LoginPageVM(INavigation navigation)
        {
            Navigation = navigation;
            apiService = new ApiService();
        }
        #endregion

        #region OBJETOS



        public string TxtEmail
        {
            get { return txtEmail; }
            set { SetValue(ref txtEmail, value); }
        }
        public string TxtContraseña
        {
            get { return txtContraseña; }
            set { SetValue(ref txtContraseña, value); }
        }


        public bool IsPassword
        {
            get
            {
                return _IsPassword;
            }
            set { SetValue(ref _IsPassword, value); }          
        }

        #endregion

        #region PROCESOS
        public async Task loginPageAsync()
        {

            var user = new User()
            {
                Email = TxtEmail,
                Password = TxtContraseña
            };

            DataUser Resultado = await apiService.Login(user);

            if (Resultado.authorizationToken == null)
            {
                await DisplayAlert("Hour", "Revisar Email y Contraseña", "Ok");
                return;
            }
           // await DisplayAlert("Hour", Resultado.authorizationToken, "Ok");

            await Navigation.PushAsync(new GamificationPage(Resultado.authorizationToken));

        }

        public async Task Deshabilitado(string opcion)
        {
           await DisplayAlert("Horus", $"La Opcion {opcion} se encuentra desahabilitada", "Ok");
        }
        #endregion

        #region COMANDOS
       // public ICommand Proceso => new Command(async () => await nextPageAsync());
       
        public ICommand ToggleIsPassword => new Command(() => IsPassword = !IsPassword);

        public ICommand InstagramLogin => new Command(async ()=> await Deshabilitado("Instagram"));
        public ICommand FacebookLogin => new Command(async () => await Deshabilitado("Facebook"));
        public ICommand RecordarContraseña => new Command(async () => await Deshabilitado("Recordar Contraseña"));

        public ICommand LoginCmd => new Command(async () => await loginPageAsync());

        #endregion
    }
}

