using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class CameraViewModel : ICameraViewModel
    {
        ICameraModel _Camera;
        int _NumberOfPictures;

        public CameraViewModel(ICameraModel c)
        {
            _Camera = c;
        }

        public int ID => _Camera.ID;

        public string Producer {
            get => _Camera.Producer;
            set => _Camera.Producer = value;
        }

        public string Make {
            get => _Camera.Make;
            set => _Camera.Make = value;
        }

        public DateTime? BoughtOn {
            get => _Camera.BoughtOn;
            set => _Camera.BoughtOn = value;
        }

        public string Notes {
            get => _Camera.Notes;
            set => _Camera.Notes = value;
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
            set => _Camera.ISOLimitGood = value;
        }

        public decimal ISOLimitAcceptable {
            get => _Camera.ISOLimitAcceptable;
            set => _Camera.ISOLimitAcceptable = value;
        }

        public ISORatings TranslateISORating(decimal iso)
        {
            if (iso == 0) return ISORatings.NotDefined;
            else if (iso <= ISOLimitGood) return ISORatings.Good;
            else if (iso <= ISOLimitAcceptable) return ISORatings.Acceptable;
            else if (iso > ISOLimitAcceptable) return ISORatings.Noisey;
            else throw new ArgumentOutOfRangeException();
        }
    }
}
