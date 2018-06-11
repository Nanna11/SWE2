using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    public class CameraModel : ICameraModel, INotifyPropertyChanged
    {
        int _ID;
        string _Producer;
        string _Make;
        DateTime? _BoughtOn;
        string _Notes;
        decimal _ISOLimitGood = 0;
        decimal _ISOLimitAcceptable = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// gets/sets the ID of camera
        /// </summary>
        public int ID
        {
            get { return _ID; }

            set { _ID = value; }
        }

        /// <summary>
        /// gets/sets producer of camera
        /// </summary>
        public string Producer {
            get { return _Producer; }

            set {
                _Producer = value;
                OnPropertyChanged("Producer");
            }
        }

        /// <summary>
        /// gets/sets make of camera
        /// </summary>
        public string Make {
            get { return _Make; }

            set {
                _Make = value;
                OnPropertyChanged("Make");
            }
        }

        /// <summary>
        /// gets/sets date on which camera was bought
        /// </summary>
        public DateTime? BoughtOn {
            get { return _BoughtOn; }

            set { _BoughtOn = value;
                OnPropertyChanged("BoughtOn");
            }
        }

        /// <summary>
        /// gets/sets notes for camera
        /// </summary>
        public string Notes {
            get { return _Notes; }

            set { _Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        /// <summary>
        /// gets/sets the ISO Limit for a good picture
        /// </summary>
        public decimal ISOLimitGood {
            get { return _ISOLimitGood; }

            set {
                _ISOLimitGood = value;
                OnPropertyChanged("ISOLimitGood");
            }
        }

        /// <summary>
        /// gets/sets the iso limit for an acceptable picture
        /// </summary>
        public decimal ISOLimitAcceptable {
            get { return _ISOLimitAcceptable; }

            set
            {
                _ISOLimitAcceptable = value;
                OnPropertyChanged("ISOLimitAcceptable");
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
