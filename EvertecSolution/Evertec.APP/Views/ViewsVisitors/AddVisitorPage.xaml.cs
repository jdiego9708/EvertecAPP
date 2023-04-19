using SISEvertec.APP.ViewModels.VMVisitors;

namespace SISEvertec.APP.Views.ViewsVisitors;

public partial class AddVisitorPage
{
	public VisitorsViewModel VisitorsViewModel { get; set; }

    public AddVisitorPage(VisitorsViewModel VisitorsViewModel)
	{
		this.VisitorsViewModel = VisitorsViewModel;

        InitializeComponent();

		this.BindingContext = VisitorsViewModel;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}