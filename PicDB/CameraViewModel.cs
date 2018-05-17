using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;
using System.ComponentModel;

namespace PicDB
{
    public class CameraViewModel : ICameraViewModel, INotifyPropertyChanged
    {
        CameraModel _Camera;
        int _NumberOfPictures;
        public event PropertyChangedEventHandler PropertyChanged;
        DateTime? _LastBoughtOn = null;
        string _LastMake = null;
        string _LastProducer = null;

        public CameraViewModel(ICameraModel c)
        {
            _Camera = (CameraModel)c;
            _Camera.PropertyChanged += SubPropertyChanged;
        }

        public int ID => _Camera.ID;

        public string Producer {
            get => _Camera.Producer;
            set
            {
                _LastProducer = _Camera.Producer;
                _Camera.Producer = value;
                OnPropertyChanged("Producer");
                OnPropertyChanged("DisplayName");
            }
        }

        public string Make {
            get => _Camera.Make;
            set
            {
                _LastMake = _Camera.Make;
                _Camera.Make = value;
                OnPropertyChanged("Make");
                OnPropertyChanged("DisplayName");
            }
        }

        public DateTime? BoughtOn {
            get => _Camera.BoughtOn;
            set
            {
                _LastBoughtOn = _Camera.BoughtOn;
                _Camera.BoughtOn = value;
                OnPropertyChanged("BoughtOn");
            }
        }

        public string Notes {
            get => _Camera.Notes;
            set
            {
                _Camera.Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public int NumberOfPictures => _NumberOfPictures;

        public bool IsValid
        {
            get
            {
                if (IsValidBoughtOn && IsValidMake && IsValidProducer) return true;
                else return false;
            }
        }

        public string ValidationSummary
        {
            get
            {
                string Summary = null;

                if (!IsValid)
                {
                    if (!IsValidBoughtOn)
                    {
                        if (Summary != null) Summary += "/";
                        Summary += "BoughtOn is invalid";
                    }
                    if (!IsValidMake)
                    {
                        if (Summary != null) Summary += "/";
                        Summary += "Make is invalid";
                    }
                    if (!IsValidProducer)
                    {
                        if (Summary != null) Summary += "/";
                        Summary += "Producer is invalid";
                    }
                }
                return Summary;
            }
        }

        public bool IsValidProducer
        {
            get
            {
                if (!string.IsNullOrEmpty(Producer) && !string.IsNullOrWhiteSpace(Producer))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsValidMake
        {
            get
            {
                if(!string.IsNullOrEmpty(Make) && !string.IsNullOrWhiteSpace(Make))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsValidBoughtOn
        {
            get
            {
                if (BoughtOn == null) return true;
                else if (BoughtOn < DateTime.Now) return true;
                else return false;
            }
        }

        public decimal ISOLimitGood {
            get => _Camera.ISOLimitGood;
            set
            {
                _Camera.ISOLimitGood = value;
                OnPropertyChanged("ISOLimitGood");
            }
        }

        public decimal ISOLimitAcceptable {
            get => _Camera.ISOLimitAcceptable;
            set
            {
                _Camera.ISOLimitAcceptable = value;
                OnPropertyChanged("ISOLimitAcceptable");
            }
        }

        public ISORatings TranslateISORating(decimal iso)
        {
            if (iso == 0) return ISORatings.NotDefined;
            else if (iso <= ISOLimitGood) return ISORatings.Good;
            else if (iso <= ISOLimitAcceptable) return ISORatings.Acceptable;
            else if (iso > ISOLimitAcceptable) return ISORatings.Noisey;
            else throw new ArgumentOutOfRangeException();
        }

        public ICameraModel CameraModel
        {
            get
            {
                return _Camera;
            }
        }

        public string DisplayName
        {
            get
            {
                return Producer + " " + Make;
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

        public void UndoUpdate()
        {
            if(!IsValidBoughtOn) BoughtOn = _LastBoughtOn;
            if(!IsValidMake) Make = _LastMake;
            if(!IsValidProducer) Producer = _LastProducer;
        }

        void SubPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender == _Camera) OnPropertyChanged(e.PropertyName);
        }
    }
}
