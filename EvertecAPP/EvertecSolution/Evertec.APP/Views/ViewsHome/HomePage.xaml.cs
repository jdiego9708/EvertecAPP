using SISEvertec.APP.ViewModels.VMHome;

namespace SISEvertec.APP.Views.ViewsHome;

public partial class HomePage 
{
	public HomeViewModel HomeViewModel { get; set; }
    public HomePage(HomeViewModel HomeViewModel)
	{
		this.HomeViewModel = HomeViewModel;

        InitializeComponent();

		this.BindingContext = HomeViewModel;
	}
}