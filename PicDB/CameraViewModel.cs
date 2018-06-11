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

        /// <summary>
        /// creates new camera view model for given ICameraModel
        /// </summary>
        /// <param name="c"></param>
        public CameraViewModel(ICameraModel c)
        {
            _Camera = (CameraModel)c;
            _Camera.PropertyChanged += SubPropertyChanged;
        }

        /// <summary>
        /// gets/sets the ID of camera
        /// </summary>
        public int ID => _Camera.ID;

        /// <summary>
        /// gets/sets producer of camera
        /// </summary>
        public string Producer {
            get => _Camera.Producer;
            set
            {
                string tmp = _Camera.Producer;
                _Camera.Producer = value;
                if (IsValidProducer) _LastProducer = tmp;
                OnPropertyChanged("Producer");
                OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        /// gets/sets make of camera
        /// </summary>
        public string Make {
            get => _Camera.Make;
            set
            {
                string tmp = _Camera.Make;
                _Camera.Make = value;
                if (IsValidMake) _LastMake = tmp;
                OnPropertyChanged("Make");
                OnPropertyChanged("DisplayName");
            }
        }

        /// <summary>
        /// gets/sets date on which camera was bought
        /// </summary>
        public DateTime? BoughtOn {
            get => _Camera.BoughtOn;
            set
            {
                DateTime? tmp = _Camera.BoughtOn;
                _Camera.BoughtOn = value;
                if (IsValidBoughtOn) _LastBoughtOn = tmp;
                OnPropertyChanged("BoughtOn");
            }
        }

        /// <summary>
        /// gets/sets notes for camera
        /// </summary>
        public string Notes {
            get => _Camera.Notes;
            set
            {
                _Camera.Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        /// <summary>
        /// returns number of pictures taken with camera
        /// </summary>
        public int NumberOfPictures => _NumberOfPictures;

        /// <summary>
        /// checks if model is valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (IsValidBoughtOn && IsValidMake && IsValidProducer) return true;
                else return false;
            }
        }

        /// <summary>
        /// creates a string containing validation result
        /// </summary>
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

        /// <summary>
        /// checks if producer is valid
        /// </summary>
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

        /// <summary>
        /// checks if make is valid
        /// </summary>
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

        /// <summary>
        /// checks if bought on is a date in the past
        /// </summary>
        public bool IsValidBoughtOn
        {
            get
            {
                if (BoughtOn == null) return true;
                else if (BoughtOn < DateTime.Now) return true;
                else return false;
            }
        }

        /// <summary>
        /// gets/sets the ISO Limit for a good picture
        /// </summary>
        public decimal ISOLimitGood {
            get => _Camera.ISOLimitGood;
            set
            {
                _Camera.ISOLimitGood = value;
                OnPropertyChanged("ISOLimitGood");
            }
        }

        /// <summary>
        /// gets/sets the iso limit for an acceptable picture
        /// </summary>
        public decimal ISOLimitAcceptable {
            get => _Camera.ISOLimitAcceptable;
            set
            {
                _Camera.ISOLimitAcceptable = value;
                OnPropertyChanged("ISOLimitAcceptable");
            }
        }

        /// <summary>
        /// translates ISO value of a picture into corresponding ISO reating for camera
        /// </summary>
        /// <param name="iso"></param>
        /// <returns></returns>
        public ISORatings TranslateISORating(decimal iso)
        {
            if (iso == 0) return ISORatings.NotDefined;
            else if (iso <= ISOLimitGood) return ISORatings.Good;
            else if (iso <= ISOLimitAcceptable) return ISORatings.Acceptable;
            else if (iso > ISOLimitAcceptable) return ISORatings.Noisey;
            else throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// returns camera model
        /// </summary>
        public ICameraModel CameraModel
        {
            get
            {
                return _Camera;
            }
        }
        /// <summary>
        /// gets display name of camera
        /// </summary>
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

        /// <summary>
        /// restore last valid state
        /// </summary>
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
