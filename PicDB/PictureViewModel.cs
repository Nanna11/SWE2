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
        ICameraViewModel _CameraViewModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public PictureViewModel(IPictureModel pm)
        {
            _PictureModel = (PictureModel)pm;
            _PictureModel.PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            if(_PictureModel.IPTC != null)
            {
                _IPTCViewModel = new IPTCViewModel(_PictureModel.IPTC);
                ((IPTCViewModel)_IPTCViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            }
            if(_PictureModel.EXIF != null)
            {
                _EXIFViewModel = new EXIFViewModel(_PictureModel.EXIF, this);
                ((EXIFViewModel)_EXIFViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            }
            if (_PictureModel.Photographer != null)
            {
                _PhotographerViewModel = new PhotographerViewModel(_PictureModel.Photographer);
                ((PhotographerViewModel)_PhotographerViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            }
            if (_PictureModel.Camera != null)
            {
                _CameraViewModel = new CameraViewModel(_PictureModel.Camera);
                ((CameraViewModel)_CameraViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            }
        }

        /// <summary>
        /// Database primary key
        /// </summary>
        public int ID => _PictureModel.ID;

        /// <summary>
        /// Name of the file
        /// </summary>
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

        /// <summary>
        /// Full file path, used to load the image
        /// </summary>
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

        /// <summary>
        /// The line below the Picture. Format: {IPTC.Headline|FileName} (by {Photographer|IPTC.ByLine}).
        /// </summary>
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

        /// <summary>
        /// The IPTC ViewModel
        /// </summary>
        public IIPTCViewModel IPTC
        {
            get
            {
                return _IPTCViewModel;
            }
        }

        /// <summary>
        /// The EXIF ViewModel
        /// </summary>
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

        /// <summary>
        /// The Photographer ViewModel
        /// </summary>
        public IPhotographerViewModel Photographer {
            get { return _PhotographerViewModel;}
            set
            {
                _PhotographerViewModel = value;
                if (_PhotographerViewModel == null) _PictureModel.Photographer = null;
                else
                {
                    _PictureModel.Photographer = ((PhotographerViewModel)_PhotographerViewModel).PhotographerModel;
                    ((PhotographerViewModel)_PhotographerViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
                }
                OnPropertyChanged("Photographer");
                OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        /// The Camera ViewModel
        /// </summary>
        public ICameraViewModel Camera
        {
            get { return _CameraViewModel; }
            set
            {
                if (value != null) _PictureModel.Camera = ((CameraViewModel)value).CameraModel;
                else _PictureModel.Camera = null;
                _CameraViewModel = value;
                ((CameraViewModel)_CameraViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            }
        }

        /// <summary>
        /// The Picture Model
        /// </summary>
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
                    if (sender == _PictureModel)
                    {
                        if (_PictureModel.Camera != null) _CameraViewModel = new CameraViewModel(_PictureModel.Camera);
                        else _CameraViewModel = null;
                        OnPropertyChanged("Camera");
                        OnPropertyChanged("EXIF");
                    }
                    break;
                case "ISOLimitGood":
                    if(sender == _CameraViewModel) OnPropertyChanged("ISOLimitGood");
                    break;
                case "ISOLimitAcceptable":
                    if (sender == _CameraViewModel) OnPropertyChanged("ISOLimitAcceptable");
                    break;
            }
        }
    }
}
