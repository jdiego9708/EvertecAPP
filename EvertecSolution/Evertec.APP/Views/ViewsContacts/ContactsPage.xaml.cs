using SISEvertec.APP.ViewModels.VMContacts;

namespace SISEvertec.APP.Views.ViewsContacts;

public partial class ContactsPage
{
	public ContactsViewModel ContactsViewModel { get; set; }

    public ContactsPage(ContactsViewModel ContactsViewModel)
	{
		this.ContactsViewModel = ContactsViewModel;

        InitializeComponent();

		this.BindingContext = ContactsViewModel;

		this.ContactsViewModel.MessagesService.CustomContext = this;
	}
}