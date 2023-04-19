using SISEvertec.APP.ViewModels.VMVisitors;

namespace SISEvertec.APP.Views.ViewsVisitors;

public partial class OptionsVisitor
{
    public VisitorsViewModel VisitorsViewModel { get; set; }
    public OptionsVisitor(VisitorsViewModel VisitorsViewModel)
	{
		this.VisitorsViewModel = VisitorsViewModel;

		InitializeComponent();

		this.BindingContext = VisitorsViewModel;
	}
}