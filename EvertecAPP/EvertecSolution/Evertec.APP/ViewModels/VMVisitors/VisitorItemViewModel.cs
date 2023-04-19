using CommunityToolkit.Mvvm.Input;
using SISEvertec.Entities.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace SISEvertec.APP.ViewModels.VMVisitors
{
    public class VisitorItemViewModel : Visitors, INotifyPropertyChanged
    {
        public VisitorItemViewModel(Visitors visitor)
        {
            this.Id = visitor.Id;
            this.Name_visitor = visitor.Name_visitor;
            this.Date_visitor = visitor.Date_visitor;
            this.Hour_visitor = visitor.Hour_visitor;
            this.Image_visitor = visitor.Image_visitor;
        }

        public ICommand SelectVisitorCommand
        {
            get
            {
                return new RelayCommand(SelectVisitor);
            }
        }
        private void SelectVisitor()
        {
            this.OnSelectVisitor?.Invoke(this, EventArgs.Empty);
        }

        private ImageSource _imageVisitor;
        public ImageSource ImageVisitor
        {
            get { return _imageVisitor; }
            set
            {
                _imageVisitor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageVisitor)));
            }
        }

        public event EventHandler OnSelectVisitor;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
