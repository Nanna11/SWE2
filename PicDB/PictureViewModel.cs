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
    class PictureViewModel : IPictureViewModel, INotifyPropertyChanged
    {
        PictureModel _PictureModel;
        IIPTCViewModel _IPTCViewModel;
        IEXIFViewModel _EXIFViewModel;
        ICameraViewModel _CameraViewModel;
        IPhotographerViewModel _PhotographerViewModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public PictureViewModel(IPictureModel pm)
        {
            _PictureModel = (PictureModel)pm;
            _IPTCViewModel = new IPTCViewModel(_PictureModel.IPTC);
            _EXIFViewModel = new EXIFViewModel(_PictureModel.EXIF);
            _CameraViewModel = new CameraViewModel(_PictureModel.Camera);
            PictureModel p = (PictureModel)_PictureModel;
            if (_PictureModel.Photographer != null) _PhotographerViewModel = new PhotographerViewModel(_PictureModel.Photographer);
        }
        public int ID => _PictureModel.ID;

        public string FileName => _PictureModel.FileName;

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
                    return null;
                }
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

        public IIPTCViewModel IPTC => _IPTCViewModel;

        public IEXIFViewModel EXIF => _EXIFViewModel;

        public IPhotographerViewModel Photographer {
            get { return _PhotographerViewModel;}
            set { _PhotographerViewModel = value; }
        }


        public ICameraViewModel Camera
        {
            get { return _CameraViewModel; }
            set { _CameraViewModel = value; }
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
    }
}
