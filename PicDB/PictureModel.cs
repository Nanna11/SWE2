using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using System.ComponentModel;

namespace PicDB
{
    public class PictureModel : IPictureModel, INotifyPropertyChanged
    {
        int _ID;
        string _FileName;
        IIPTCModel _IPTC = new IPTCModel();
        IEXIFModel _EXIF = new EXIFModel();
        ICameraModel _Camera;
        IPhotographerModel _Photographer;
        public event PropertyChangedEventHandler PropertyChanged;

        public PictureModel(string fn)
        {
            FileName = fn;
        }

        /// <summary>
        /// Database primary key
        /// </summary>
        public int ID {
            get => _ID;
            set { _ID = value; }
            }

        /// <summary>
        /// Filename of the picture, without path.
        /// </summary>
        public string FileName {
            get => _FileName;
            set { _FileName = value;}
        }

        /// <summary>
        /// IPTC information
        /// </summary>
        public IIPTCModel IPTC {
            get => _IPTC;
            set { _IPTC = value; }
            }

        /// <summary>
        /// EXIF information
        /// </summary>
        public IEXIFModel EXIF {
            get => _EXIF;
            set { _EXIF = value; }
        }

        /// <summary>
        /// The camera (optional)
        /// </summary>
        public ICameraModel Camera {
            get => _Camera;
            set
            {
                _Camera = value;
                OnPropertyChanged("Camera");
            }
        }

        /// <summary>
        /// The photographer (optional)
        /// </summary>
        public IPhotographerModel Photographer
        {
            get => _Photographer;
            set => _Photographer = value;
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
