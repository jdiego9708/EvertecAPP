using SISEvertec.APP.ViewModels.VMLogin;

namespace SISEvertec.APP.Views.ViewsLogin;

public partial class LoginPage
{
	public LoginViewModel LoginViewModel { get; set; }
    public LoginPage(LoginViewModel LoginViewModel)
	{
		this.LoginViewModel = LoginViewModel;

		InitializeComponent();

		this.BindingContext = LoginViewModel;

		this.LoginViewModel.MessagesService.CustomContext = this;
	}
}