using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PicDB
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        SearchViewModel _Search;
        IPictureListViewModel _List;
        CameraListViewModel _CameraList;
        PhotographerListViewModel _PhotographerList;
        BusinessLayer bl = new BusinessLayer();
        UpdateSources _UpdatePicture = null;
        UpdateSources _UpdatePhotographer = null;
        UpdateSources _UpdateCamera = null;
        ActionCommand _SavePicture = null;
        ActionCommand _SavePhotographer = null;
        ActionCommand _SaveCamera = null;
        ActionCommand _DeletePhotographer = null;
        ActionCommand _DeleteCamera = null;
        ActionCommand _AddPhotographer = null;
        ActionCommand _AddCamera = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel(UpdateSources UpdatePicture, UpdateSources UpdatePhotographer, UpdateSources UpdateCamera)
        {
            _UpdatePicture = UpdatePicture;
            _UpdatePhotographer = UpdatePhotographer;
            _UpdateCamera = UpdateCamera;
            bl.Error += (message) => System.Windows.MessageBox.Show(message);
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

        public ICommand SavePicture
        {
            get
            {
                if(_SavePicture == null)
                {
                    _SavePicture = new ActionCommand(() =>
                    {
                        if (_UpdatePicture != null) _UpdatePicture();
                        bl.CurrentPictureChanged(CurrentPicture);
                    });
                }
                return _SavePicture;
            }
        }

        public ICommand SavePhotographer
        {
            get
            {
                if (_SavePhotographer == null)
                {
                    return _SavePhotographer = new ActionCommand(() =>
                    {
                        if (_UpdatePhotographer != null) _UpdatePhotographer();
                        bl.CurrentPhotographerChanged(PhotographerList.CurrentPhotographer);
                    });
                }
                return _SavePicture;
            }
        }

        public ICommand SaveCamera
        {
            get
            {
                if (_SaveCamera == null)
                {
                    return _SaveCamera= new ActionCommand(() =>
                    {
                        if (_UpdatePhotographer != null) _UpdateCamera();
                        bl.CurrentCameraChanged(CameraList.CurrentCamera);
                    });
                }
                return _SaveCamera;
            }
        }

        public ICommand DeleteCamera
        {
            get
            {
                if (_DeleteCamera == null)
                {
                    return _DeleteCamera = new ActionCommand(() =>
                    {
                        bl.DeleteCamera(CameraList.CurrentCamera.ID);
                        CameraList = new CameraListViewModel(bl.GetCameras());
                    });
                }
                return _DeleteCamera;
            }
        }

        public ICommand DeletePhotographer
        {
            get
            {
                if (_DeletePhotographer == null)
                {
                    return _DeletePhotographer = new ActionCommand(() =>
                    {
                        bl.DeletePhotographer(PhotographerList.CurrentPhotographer.ID);
                        PhotographerList = new PhotographerListViewModel(bl.GetPhotographers());
                    });
                }
                return _DeletePhotographer;
            }
        }

        public ICommand AddPhotographer
        {
            get
            {
                if (_AddPhotographer == null)
                {
                    return _AddPhotographer = new ActionCommand(() =>
                    {
                        PhotographerModel p = new PhotographerModel();
                        PhotographerList.Add(p);
                        PhotographerList.CurrentPhotographer.LastName = "unnamed";
                        bl.Save(p);
                    });
                }
                return _AddPhotographer;
            }
        }

        public ICommand AddCamera
        {
            get
            {
                if (_AddCamera == null)
                {
                    return _AddCamera = new ActionCommand(() =>
                    {
                        CameraModel p = new CameraModel();
                        CameraList.Add(p);
                        CameraList.CurrentCamera.Producer = "<<Producer>>";
                        CameraList.CurrentCamera.Make = "<<Make>>";
                        bl.Save(p);
                    });
                }
                return _AddCamera;
            }
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
                ((PictureListViewModel)List).SetCameras(CameraList);
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
