using SISEvertec.APP.ViewModels.VMVisitors;

namespace SISEvertec.APP.Views.ViewsVisitors;

public partial class VisitorsPage
{
	public VisitorsViewModel VisitorsViewModel { get; set; }
    public VisitorsPage(VisitorsViewModel VisitorsViewModel)
	{
		this.VisitorsViewModel = VisitorsViewModel;

		InitializeComponent();

		this.BindingContext = VisitorsViewModel;

		this.VisitorsViewModel.MessagesService.CustomContext = this;
	}
}