namespace SISEvertec.APP.Views.ViewsMessages;

public partial class ErrorPage
{
	public ErrorPage()
	{
		InitializeComponent();
	}
    public void SetDialogInfo(string informacion, string texto_boton = "Entendido")
    {
        this.lblInfo.Text = informacion;
        this.btnNext.Text = texto_boton;
    }
    private void BtnNext_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}