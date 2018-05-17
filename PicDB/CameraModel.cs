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

        public int ID
        {
            get { return _ID; }

            set { _ID = value; }
        }

        public string Producer {
            get { return _Producer; }

            set {
                _Producer = value;
                OnPropertyChanged("Producer");
            }
        }

        public string Make {
            get { return _Make; }

            set {
                _Make = value;
                OnPropertyChanged("Make");
            }
        }

        public DateTime? BoughtOn {
            get { return _BoughtOn; }

            set { _BoughtOn = value;
                OnPropertyChanged("BoughtOn");
            }
        }

        public string Notes {
            get { return _Notes; }

            set { _Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public decimal ISOLimitGood {
            get { return _ISOLimitGood; }

            set {
                _ISOLimitGood = value;
                OnPropertyChanged("ISOLimitGood");
            }
        }

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
