using System.ComponentModel;

namespace SISEvertec.APP.ViewModels.VMMain
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this ?? new BaseViewModel(), new PropertyChangedEventArgs(propertyName));
        }
    }
}
