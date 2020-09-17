using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLatihan.Commands;
using WpfLatihan.Models;

namespace WpfLatihan.ViewModels
{
    public class PelangganViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        PelangganService ObjPelangganService;
        public PelangganViewModel()
        {
            ObjPelangganService = new PelangganService();
            LoadData();
            CurrentPelanggan = new Pelanggan();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        private ObservableCollection<Pelanggan> pelanggansList;

        public ObservableCollection<Pelanggan> PelanggansList
        {
            get { return pelanggansList; }
            set { pelanggansList = value; OnPropertyChanged("PelanggansList"); }
        }

        private void LoadData()
        {
            PelanggansList = new ObservableCollection<Pelanggan>(ObjPelangganService.GetAll());
        }
        #endregion

        private Pelanggan currentPelanggan;

        public Pelanggan CurrentPelanggan
        {
            get { return currentPelanggan; }
            set { currentPelanggan = value; OnPropertyChanged("CurrentPelanggan"); }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region SaveOperation
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                var IsSaved = ObjPelangganService.AddPelanggan(CurrentPelanggan);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";

            }
            catch (Exception ex)
            {
                Message = ex.Message;

            }
        }
        #endregion

        #region SearchOperation
        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            try
            {
                var ObjPelanggan = ObjPelangganService.Search(CurrentPelanggan.Id);

                if (ObjPelanggan != null)
                {
                    CurrentPelanggan.Name = ObjPelanggan.Name;
                    CurrentPelanggan.Age = ObjPelanggan.Age;
                }
                else
                {
                    Message = "Pelanggan not found";
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        
        public void Update()
        {
            try
            {
                bool IsUpdated = ObjPelangganService.UpdatePelanggan(CurrentPelanggan);
                if (IsUpdated)
                {
                    Message = "Pelanggan Updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operations failed";
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;

            }
        }
        #endregion

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        public void Delete()
        {
            try
            {
                var IsDeleted = ObjPelangganService.DeletePelanggan(CurrentPelanggan.Id);
                if (IsDeleted)
                {
                    Message = "Pelanggan Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete Operations Failed";
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;

            }
        }

    }
}
