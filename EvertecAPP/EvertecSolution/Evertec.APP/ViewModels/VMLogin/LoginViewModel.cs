using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SISEvertec.APP.Services;
using SISEvertec.APP.ViewModels.VMContacts;
using SISEvertec.APP.ViewModels.VMHome;
using SISEvertec.APP.ViewModels.VMMain;
using SISEvertec.APP.ViewModels.VMVisitors;
using SISEvertec.APP.Views.ViewsMessages;
using SISEvertec.Entities.Helpers.Interfaces;
using SISEvertec.Entities.ModelsBinding;
using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec;
using System.Windows.Input;

namespace SISEvertec.APP.ViewModels.VMLogin
{
    public class LoginViewModel : BaseViewModel
    {
        #region CONSTRUCTOR
        private readonly EvertecDBContext EvertecDBContext;
        public IRestHelper RestHelper { get; set; }
        public MessagesService MessagesService { get; set; }
        public ContactsViewModel ContactsViewModel { get; set; }
        public VisitorsViewModel VisitorsViewModel { get; set; }      
        public HomeViewModel HomeViewModel { get; set; }
        public LoginViewModel(IRestHelper IRestHelper,
            MessagesService MessagesService,
            ContactsViewModel ContactsViewModel,
            VisitorsViewModel VisitorsViewModel,
            EvertecDBContext evertecDBContext,
            HomeViewModel HomeViewModel)
        {
            this.RestHelper = IRestHelper;
            this.MessagesService = MessagesService;
            this.ContactsViewModel = ContactsViewModel;
            this.VisitorsViewModel = VisitorsViewModel;
            this.EvertecDBContext = evertecDBContext;       
            this.HomeViewModel = HomeViewModel;

            this.IsPasswordVisible = true;
        }
        #endregion

        #region METHODS      
        private async Task Login()
        {
            try
            {
                this.UserName = "user.demo@mail.com";
                this.Password = "Password2023*";

                if (string.IsNullOrEmpty(this.UserName))
                    throw new Exception("El usuario no puede estar vacío");

                if (string.IsNullOrEmpty(this.Password))
                    throw new Exception("La clave no puede estar vacía");

                if (!this.UserName.Equals("user.demo@mail.com"))
                    throw new Exception("El usuario ingresado no es correcto");

                if (!this.Password.Equals("Password2023*"))
                    throw new Exception("La clave ingresada no es correcta");

                RestResponseModel response = await this.RestHelper.CallMethodGetAsync("/UserSignIn.json");

                if (!response.Success)
                    throw new Exception("Error iniciando sesión");

                JToken jToken = JToken.Parse(response.Message);

                LoginDataModel loginData = JsonConvert.DeserializeObject<LoginDataModel>(jToken.ToString());

                await this.EvertecDBContext.CreateBDSQLite();
             
                await this.VisitorsViewModel.LoadVisitors();

                await this.ContactsViewModel.LoadContacts();

                await this.HomeViewModel.LoadLastChanged();

                this.HomeViewModel.IsDarkMode = false;

                this.HomeViewModel.IsSpanish = true;
            
                await this.MessagesService.ShowDialogOk($"¡Inicio de sesión correcto! {Environment.NewLine}" +
                $"Usuario: {loginData.FirstName} {loginData.SurName} {Environment.NewLine}" +
                $"Token: {loginData.AuthorizationToken}");

                await Shell.Current.GoToAsync("//HomePage", true);
            }
            catch (Exception ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
        }
        #endregion

        #region COMMANDS
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            var taskLogin = this.Login();

                            await MainThread.InvokeOnMainThreadAsync(async () => await this.MessagesService.ShowDialogWait(new List<Task>()
                            {
                                taskLogin,
                            }, "Iniciando sesión..."));
                        }
                    );
            }
        }
        public ICommand ChangePasswordVisibleCommand
        {
            get
            {
                return new RelayCommand
                    (
                        () =>
                        {
                            if (this.IsPasswordVisible)
                                this.IsPasswordVisible = false;
                            else
                                this.IsPasswordVisible = true;
                        }
                    );
            }
        }
        public ICommand RememberPasswordCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.MessagesService.ShowDialogOk("Botón de prueba, contraseña recordada");
                        }
                    );
            }
        }
        public ICommand FacebookCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.MessagesService.ShowDialogOk("Botón de prueba, Facebook");
                        }
                    );
            }
        }
        public ICommand InstagramCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.MessagesService.ShowDialogOk("Botón de prueba, Instagram");
                        }
                    );
            }
        }
        public ICommand GoogleCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.MessagesService.ShowDialogOk("Botón de prueba, Google");
                        }
                    );
            }
        }
        #endregion

        #region PROPERTIES
        private string _userName;
        private string _password;
        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                _isPasswordVisible = value;
                OnPropertyChanged(nameof(IsPasswordVisible));
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion
    }
}
