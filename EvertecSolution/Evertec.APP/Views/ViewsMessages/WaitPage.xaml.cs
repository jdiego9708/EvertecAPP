namespace SISEvertec.APP.Views.ViewsMessages;

public partial class WaitPage
{
	public WaitPage()
	{
		InitializeComponent();
        this.Opened += WaitPage_Opened;
	}
    private async void WaitPage_Opened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        if (this.TareasPendientes == null)
        {
            this.Close();
            return;
        }

        if (this.TareasPendientes.Count < 1)
        {
            this.Close();
            return;
        }

        await Task.WhenAll(this.TareasPendientes);

        this.Close();
    }
    public void SetDialogInfo(string informacion = "Cargando...", string titulo = "Espere...")
    {
        this.lblTitulo.Text = titulo;
        this.lblInformacion.Text = informacion;
    }
    public List<Task> TareasPendientes { get; set; }
}