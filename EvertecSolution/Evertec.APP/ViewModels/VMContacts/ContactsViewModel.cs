using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SISEvertec.APP.ViewModels.VMMain;
using SISEvertec.APP.Views.ViewsMessages;
using SISEvertec.Entities.Helpers.Interfaces;
using SISEvertec.Entities.ModelsBinding;
using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using SkiaSharp;
using Svg.Skia;

namespace SISEvertec.APP.ViewModels.VMContacts
{
    public class ContactsViewModel : BaseViewModel
    {
        #region CONSTRUCTOR
        public IRestHelper RestHelper { get; set; }
        public MessagesService MessagesService { get; set; }
        public ContactsViewModel(IRestHelper IRestHelper,
            MessagesService MessagesService)
        {
            this.RestHelper = IRestHelper;
            this.MessagesService = MessagesService;
        }
        #endregion

        #region METHODS      
        public async Task LoadContacts()
        {
            try
            {
                RestResponseModel response = await this.RestHelper.CallMethodGetAsync("/GetUser.json");

                if (!response.Success)
                    throw new Exception("Error iniciando sesión");

                JToken jToken = JToken.Parse(response.Message);

                List<ContactBindingModel> contacts = JsonConvert.DeserializeObject<List<ContactBindingModel>>(jToken.ToString());

                List<ContactItemViewModel> contactsItem = new();
                foreach (ContactBindingModel contact in contacts)
                {
                    ContactItemViewModel contactItem = new(contact);

                    if (contact.Id % 2 == 0)
                        contactItem.IsPar = true;
                    else
                        contactItem.IsPar = false;

                    contactItem.OnSelectContact += ContactItem_OnSelectContact;

                    contactsItem.Add(contactItem);
                }

                this.ContactsList = contacts;

                this.ContactsViewList = new(contactsItem);

                this.InfoContacts = contactsItem.Count.ToString();

                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
        }
        private async void ContactItem_OnSelectContact(object sender, EventArgs e)
        {
            ContactItemViewModel contact = (ContactItemViewModel)sender;
            await this.MessagesService.ShowDialogOk($"Usuario seleccionado: {contact.Name} {Environment.NewLine} " +
                $"Fecha de creación: {contact.CreatedAt:yyyy-MM-dd}");
        }
        #endregion

        #region COMMANDS
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.LoadContacts();
                        }
                    );
            }
        }
        #endregion

        #region PROPERTIES
        private List<ContactBindingModel> _contactsList;
        private ObservableCollection<ContactItemViewModel> _contactsViewList;
        private string _infoContacts;
        private bool _isRefreshing;
        public List<ContactBindingModel> ContactsList
        {
            get => _contactsList;
            set
            {
                _contactsList = value;
                OnPropertyChanged(nameof(ContactsList));
            }
        }
        public ObservableCollection<ContactItemViewModel> ContactsViewList
        {
            get => _contactsViewList;
            set
            {
                _contactsViewList = value;
                OnPropertyChanged(nameof(ContactsViewList));
            }
        }
        public string InfoContacts
        {
            get => _infoContacts;
            set
            {
                _infoContacts = value;
                OnPropertyChanged(nameof(InfoContacts));
            }
        }
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        #endregion
    }
}
