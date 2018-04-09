using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PicDB
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        SearchViewModel _Search;
        IPictureListViewModel _List;
        CameraListViewModel _CameraList;
        PhotographerListViewModel _PhotographerList;
        BusinessLayer bl = new BusinessLayer();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            bl.Sync();
            _List = new PictureListViewModel(bl.GetPictures(null, null, null, null));
            _CameraList = new CameraListViewModel(bl.GetCameras());
            ((PictureListViewModel)List).SetCameras(CameraList);
            _PhotographerList = new PhotographerListViewModel(bl.GetPhotographers());
            ((PictureListViewModel)List).SetPhotographers(PhotographerList);
            _Search = new SearchViewModel();
            //nicht neu instanzieren
            _Search.SearchActivated += (s,e)=> List = new PictureListViewModel(bl.GetPictures(e.Searchtext, null, null, null));

        }

        public IPictureViewModel CurrentPicture
        {
            get
            {
                return List.CurrentPicture;
            }
        }

        public int CurrentIndex
        {
            get
            {
                return List.CurrentIndex;
            }

            set
            {
                ((PictureListViewModel)List).CurrentIndex = value;
                OnPropertyChanged("CurrentPicture");
                OnPropertyChanged("CurrentIndex");
            }
        }

        public IPictureListViewModel List
        {
            get
            {
                return _List;
            }

            set
            {
                _List = value;
                ((PictureListViewModel)List).SetPhotographers(PhotographerList);
                OnPropertyChanged("List");
                OnPropertyChanged("CurrentPicture");
            }
        }

        public PhotographerListViewModel PhotographerList
        {
            get
            {
                return _PhotographerList;
            }

            set
            {
                _PhotographerList = value;
                ((PictureListViewModel)List).SetPhotographers(PhotographerList);
                OnPropertyChanged("PhotographerList");
            }
        }

        public CameraListViewModel CameraList
        {
            get
            {
                return _CameraList;
            }

            set
            {
                _CameraList = value;
                OnPropertyChanged("CameraList");
            }
        }

        public ISearchViewModel Search => _Search;
        

        internal void CurrentPictureChanged()
        {
            bl.CurrentPictureChanged(CurrentPicture);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
