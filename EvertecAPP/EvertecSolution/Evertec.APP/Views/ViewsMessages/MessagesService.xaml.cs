using CommunityToolkit.Maui.Views;
using SISEvertec.APP.ViewModels.VMVisitors;
using SISEvertec.APP.Views.ViewsVisitors;

namespace SISEvertec.APP.Views.ViewsMessages;

public partial class MessagesService
{
	public MessagesService()
	{
		InitializeComponent();
	}
    public Page CustomContext { get; set; }
    private WaitPage WaitPage;
    private AddVisitorPage AddVisitorPage;
    private OptionsVisitor OptionsVisitor;
    public async Task ShowDialogOk(string mensaje)
	{
		if (this.CustomContext == null)
			return;

		OkPage okPage = new()
		{
			VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End,
		};

		okPage.SetDialogInfo(mensaje);

		await this.CustomContext.ShowPopupAsync(okPage);
	}
    public async Task ShowDialogError(string mensaje)
    {
        if (this.CustomContext == null)
            return;

        OkPage okPage = new()
        {
            VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End,
        };

        okPage.SetDialogInfo(mensaje);

        await this.CustomContext.ShowPopupAsync(okPage);
    }
    public async Task ShowDialogWait(List<Task> tareas, string informacion = "Cargando...", string titulo = "Espere...")
    {
        if (this.CustomContext == null)
            return;

        this.WaitPage = new()
        {
            VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End,
            TareasPendientes = tareas,
        };

        this.WaitPage.SetDialogInfo(informacion, titulo);

        await this.CustomContext.ShowPopupAsync(this.WaitPage);
    }
    public async Task ShowDialogOptionsVisitors(VisitorsViewModel visitorsViewModel)
    {
        if (this.CustomContext == null)
            return;

        this.OptionsVisitor = new(visitorsViewModel)
        {
            VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End
        };

        await this.CustomContext.ShowPopupAsync(this.OptionsVisitor);
    }
    public async Task ShowDialogAddVisitors(VisitorsViewModel visitorsViewModel)
    {
        if (this.CustomContext == null)
            return;

        this.AddVisitorPage = new(visitorsViewModel)
        {
            VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End
        };

        await this.CustomContext.ShowPopupAsync(this.AddVisitorPage);
    }
    public void CloseOptionsVisitors()
    {
        if (this.OptionsVisitor == null)
            return;

        this.OptionsVisitor.Close();
    }
    public void CloseAddVisitors()
    {
        if (this.AddVisitorPage == null)
            return;

        this.AddVisitorPage.Close();
    }
}