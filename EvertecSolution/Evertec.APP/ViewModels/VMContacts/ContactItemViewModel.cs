using CommunityToolkit.Mvvm.Input;
using SISEvertec.Entities.ModelsBinding;
using System.ComponentModel;
using System.Windows.Input;

namespace SISEvertec.APP.ViewModels.VMContacts
{
    public class ContactItemViewModel : ContactBindingModel, INotifyPropertyChanged
    {
        public ContactItemViewModel(ContactBindingModel contact)
        {
            this.Id = contact.Id;
            this.Info = contact.Info;
            this.CreatedAt = contact.CreatedAt;
            this.Avatar = contact.Avatar;
            this.Name = contact.Name;
        }

        public ICommand SelectContactCommand
        {
            get
            {
                return new RelayCommand(SelectContact);
            }
        }
        private void SelectContact()
        {
            this.OnSelectContact?.Invoke(this, EventArgs.Empty);
        }

        private bool _isPar;
        public bool IsPar
        {
            get { return _isPar; }
            set
            {
                _isPar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPar)));
            }
        }

        public event EventHandler OnSelectContact;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
