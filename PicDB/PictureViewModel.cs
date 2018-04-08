using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace PicDB
{
    public class PictureViewModel : IPictureViewModel, INotifyPropertyChanged
    {
        PictureModel _PictureModel;
        IIPTCViewModel _IPTCViewModel;
        IEXIFViewModel _EXIFViewModel;
        IPhotographerViewModel _PhotographerViewModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public PictureViewModel(IPictureModel pm)
        {
            _PictureModel = (PictureModel)pm;
            _PictureModel.PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            _IPTCViewModel = new IPTCViewModel(_PictureModel.IPTC);
            _EXIFViewModel = new EXIFViewModel(_PictureModel.EXIF);
            ((IPTCViewModel)_IPTCViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            PictureModel p = (PictureModel)_PictureModel;
            if (_PictureModel.Photographer != null)
            {
                _PhotographerViewModel = new PhotographerViewModel(_PictureModel.Photographer);
                ((PhotographerViewModel)_PhotographerViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            }
        }
        public int ID => _PictureModel.ID;

        public string FileName
        {
            get
            {
                 return _PictureModel.FileName;
            }

            set
            {
                _PictureModel.FileName = value;
                OnPropertyChanged("FileName");
                OnPropertyChanged("FilePath");
                OnPropertyChanged("DisplayName");
            }
        }

        public string FilePath
        {
            get
            {
                string folder;
                try
                {
                    folder = GlobalInformation.Instance.Folder;
                }
                catch (SingletonNotInitializedException)
                {
                    throw new PathNotSetException();
                }
                if (String.IsNullOrEmpty(folder)) throw new PathNotSetException("Path was empty");
                string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.Combine(deploypath, folder, FileName);
            }
        }

        public string DisplayName
        {
            get
            {
                string dn = null;

                if (!string.IsNullOrEmpty(IPTC.Headline)) dn += IPTC.Headline;
                else dn += FileName;

                dn += " (by ";

                if (Photographer != null)
                {
                    if (Photographer.FirstName != null) dn += Photographer.FirstName + " ";
                    dn += Photographer.LastName;
                }
                else
                {
                    dn += IPTC.ByLine;
                }
                dn += ")";
                return dn;

            }

        }

        public IIPTCViewModel IPTC
        {
            get
            {
                return _IPTCViewModel;
            }
        }

        public IEXIFViewModel EXIF
        {
            get
            {
                return _EXIFViewModel;
            }

            set
            {
                _EXIFViewModel = value;
                if(_EXIFViewModel.Camera != null) _PictureModel.Camera = ((CameraViewModel)_EXIFViewModel.Camera).CameraModel;
                ((EXIFViewModel)_EXIFViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
                OnPropertyChanged("EXIF");
                OnPropertyChanged("Camera");
            }
        }

        public IPhotographerViewModel Photographer {
            get { return _PhotographerViewModel;}
            set
            {
                _PhotographerViewModel = value;
                ((PhotographerViewModel)_PhotographerViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
                OnPropertyChanged("Photographer");
                OnPropertyChanged("DisplayName");
            }
        }


        public ICameraViewModel Camera
        {
            get { return EXIF?.Camera; }
            set
            {
                if (EXIF != null) EXIF.Camera = value;
                else
                {
                    EXIF = new EXIFViewModel(new EXIFModel());
                    EXIF.Camera = value;
                }
                OnPropertyChanged("Camera");
            }
        }

        public PictureModel PictureModel
        {
            get
            {
                return _PictureModel;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SubPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ByLine":
                    OnPropertyChanged("DisplayName");
                    break;
                case "Headline":
                    OnPropertyChanged("DisplayName");
                    break;
                case "FirstName":
                    OnPropertyChanged("DisplayName");
                    break;
                case "LastName":
                    OnPropertyChanged("DisplayName");
                    break;
                case "Camera":
                    OnPropertyChanged("Camera");
                    if (sender == EXIF) _PictureModel.Camera = ((CameraViewModel)Camera).CameraModel;
                    else if (sender == _PictureModel) Camera = new CameraViewModel(_PictureModel.Camera);
                    break;
            }
        }
    }
}
