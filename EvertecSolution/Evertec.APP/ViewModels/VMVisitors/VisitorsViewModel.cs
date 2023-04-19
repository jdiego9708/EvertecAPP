using CommunityToolkit.Mvvm.Input;
using SISEvertec.APP.Services.Interfaces;
using SISEvertec.APP.ViewModels.VMMain;
using SISEvertec.APP.Views.ViewsMessages;
using SISEvertec.Entities.Helpers.Interfaces;
using SISEvertec.Entities.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace SISEvertec.APP.ViewModels.VMVisitors
{
    public class VisitorsViewModel : BaseViewModel
    {
        #region CONSTRUCTOR
        public IRestHelper RestHelper { get; set; }
        public MessagesService MessagesService { get; set; }
        public IVisitorsService VisitorsService { get; set; }
        public VisitorsViewModel(IRestHelper IRestHelper,
            MessagesService MessagesService,
            IVisitorsService VisitorsService)
        {
            this.RestHelper = IRestHelper;
            this.MessagesService = MessagesService;
            this.VisitorsService = VisitorsService;
        }
        #endregion

        #region METHODS      
        public async Task LoadVisitors()
        {
            try
            {
                this.VisitorsList = new();
                this.VisitorsViewList = new();

                List<Visitors> visitors = await this.VisitorsService.GetAll();

                if (visitors == null)
                    throw new Exception("No se encontraron visitantes");

                if (visitors.Count < 1)
                    throw new Exception("No se encontraron visitantes");

                this.VisitorsList = visitors;

                List<VisitorItemViewModel> visitorsItems = new();

                foreach (Visitors visitor in visitors)
                {
                    VisitorItemViewModel visitorItem = new(visitor);
                    visitorItem.OnSelectVisitor += VisitorItem_OnSelectVisitor;

                    if (!string.IsNullOrWhiteSpace(visitor.Image_visitor))
                    {
                        byte[] imageBytes = Convert.FromBase64String(visitor.Image_visitor);
                        var stream = new MemoryStream(imageBytes);
                        visitorItem.ImageVisitor = ImageSource.FromStream(() => stream);
                    }
            
                    visitorsItems.Add(visitorItem);
                }

                this.VisitorsViewList = new(visitorsItems);

                this.InfoVisitors = visitors.Count.ToString();

                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.VisitorsViewList = new();
                await this.MessagesService.ShowDialogError(ex.Message);
            }
        }
        private async void VisitorItem_OnSelectVisitor(object sender, EventArgs e)
        {
            VisitorItemViewModel visitor = (VisitorItemViewModel)sender;

            this.VisitorSelected = visitor;

            await this.MessagesService.ShowDialogOptionsVisitors(this);
        }
        private async Task AddVisitor()
        {
            try
            {

                if (!MediaPicker.Default.IsCaptureSupported)
                    return;

                var photoFile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Toma una foto"
                });

                if (photoFile == null)
                    throw new Exception("Error iniciando la cámara");

                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photoFile.FileName);

                using Stream sourceStream = await photoFile.OpenReadAsync();

                using MemoryStream memoryStream = new();

                await sourceStream.CopyToAsync(memoryStream);

                byte[] byteArray = memoryStream.ToArray();

                string fotoBase64 = Convert.ToBase64String(byteArray);

                this.VisitorAdd = new()
                {
                    Name_visitor = "",
                    Image_visitor = fotoBase64,
                    Date_visitor = DateTime.Now,
                    Hour_visitor = DateTime.Now.TimeOfDay,
                };

                await this.MessagesService.ShowDialogAddVisitors(this);
            }
            catch (FeatureNotSupportedException ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
            catch (PermissionException ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
            catch (Exception ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
        }
        private async Task ContinueAddVisitor()
        {
            try
            {
                if (this.VisitorAdd == null)
                    throw new Exception("Debe escribir un nombre de visitante para continuar");

                if (string.IsNullOrEmpty(this.VisitorAdd.Name_visitor))
                    throw new Exception("Debe escribir un nombre de visitante para continuar");

                if (string.IsNullOrEmpty(this.VisitorAdd.Image_visitor))
                    throw new Exception("Debe seleccionar una imagen de visitante para continuar");

                if (this.VisitorAdd.Id != 0)
                {
                    await this.VisitorsService.Update(this.VisitorAdd);
                    this.MessagesService.CloseOptionsVisitors();
                    await this.MessagesService.ShowDialogOk("¡Visitante actualizado con éxito!");                  
                }
                else
                {
                    await this.VisitorsService.Add(this.VisitorAdd);
                    await this.MessagesService.ShowDialogOk("¡Visitante agregado con éxito!");
                }
              
                await this.LoadVisitors();
              
                this.MessagesService.CloseAddVisitors();
            }
            catch (FeatureNotSupportedException ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
            catch (PermissionException ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
            catch (Exception ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
        }
        private async Task DeleteVisitor()
        {
            if (this.VisitorSelected == null)
                return;

            await this.VisitorsService.Delete(this.VisitorSelected.Id);

            await this.MessagesService.ShowDialogOk("¡Visitante eliminado con éxito!");

            await this.LoadVisitors();

            this.MessagesService.CloseOptionsVisitors();
        }
        private async Task EditVisitor()
        {
            if (this.VisitorSelected == null)
                return;

            if (!MediaPicker.Default.IsCaptureSupported)
                return;

            var photoFile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Toma una foto"
            });

            if (photoFile == null)
                throw new Exception("Error iniciando la cámara");

            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photoFile.FileName);

            using Stream sourceStream = await photoFile.OpenReadAsync();

            using MemoryStream memoryStream = new();

            await sourceStream.CopyToAsync(memoryStream);

            byte[] byteArray = memoryStream.ToArray();

            string fotoBase64 = Convert.ToBase64String(byteArray);

            this.VisitorAdd = new()
            {
                Id = this.VisitorSelected.Id,
                Name_visitor = "",
                Image_visitor = fotoBase64,
                Date_visitor = this.VisitorSelected.Date_visitor,
                Hour_visitor = this.VisitorSelected.Hour_visitor,
            };

            await this.MessagesService.ShowDialogAddVisitors(this);
        }
        #endregion

        #region COMMANDS
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.LoadVisitors();
                        }
                    );
            }
        }
        public ICommand AddVisitorCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.AddVisitor();
                        }
                    );
            }
        }
        public ICommand ContinueAddVisitorCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.ContinueAddVisitor();
                        }
                    );
            }
        }
        public ICommand DeleteVisitorCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.DeleteVisitor();
                        }
                    );
            }
        }
        public ICommand EditVisitorCommand
        {
            get
            {
                return new RelayCommand
                    (
                        async () =>
                        {
                            await this.EditVisitor();
                        }
                    );
            }
        }
        #endregion

        #region PROPERTIES
        private List<Visitors> _visitorsList;
        private ObservableCollection<VisitorItemViewModel> _visitorsViewList;
        private VisitorItemViewModel _visitorSelected;
        private string _infoVisitors;
        private Visitors _visitorAdd;
        private bool _isRefreshing;
        public Visitors VisitorAdd
        {
            get => _visitorAdd;
            set
            {
                _visitorAdd = value;
                OnPropertyChanged(nameof(VisitorAdd));
            }
        }
        public VisitorItemViewModel VisitorSelected
        {
            get => _visitorSelected;
            set
            {
                _visitorSelected = value;
                OnPropertyChanged(nameof(VisitorSelected));
            }
        }
        public List<Visitors> VisitorsList
        {
            get => _visitorsList;
            set
            {
                _visitorsList = value;
                OnPropertyChanged(nameof(VisitorsList));
            }
        }
        public ObservableCollection<VisitorItemViewModel> VisitorsViewList
        {
            get => _visitorsViewList;
            set
            {
                _visitorsViewList = value;
                OnPropertyChanged(nameof(VisitorsViewList));
            }
        }
        public string InfoVisitors
        {
            get => _infoVisitors;
            set
            {
                _infoVisitors = value;
                OnPropertyChanged(nameof(InfoVisitors));
            }
        }
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        #endregion
    }
}
