using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class PhotographerViewModel : IPhotographerViewModel
    {
        IPhotographerModel _PhotographerModel;
        int _NumberOfPictures;

        public PhotographerViewModel(IPhotographerModel pm)
        {
            _PhotographerModel = pm;
        }
        public int ID => _PhotographerModel.ID;

        public string FirstName {
            get => _PhotographerModel.FirstName;
            set => _PhotographerModel.FirstName = value;
        }

        public string LastName {
            get => _PhotographerModel.LastName;
            set => _PhotographerModel.LastName = value;
        }

        public DateTime? BirthDay {
            get => _PhotographerModel.BirthDay;
            set => _PhotographerModel.BirthDay = value;
        }

        public string Notes {
            get => _PhotographerModel.Notes;
            set => _PhotographerModel.Notes = value;
        }

        public int NumberOfPictures => _NumberOfPictures;

        public bool IsValid
        {
            get
            {
                if (IsValidBirthDay && IsValidLastName) return true;
                else return false;
            }
        }

        public string ValidationSummary
        {
            get
            {
                string Summary = null;
                if (IsValid)
                {
                    Summary = "Photographer View Model is valid";
                }
                else
                {
                    if (!IsValidLastName)
                    {
                        if (Summary != null) Summary += "/";
                        Summary += "Last Name is invalid";
                    }
                    if (!IsValidBirthDay)
                    {
                        if (Summary != null) Summary += "/";
                        Summary += "BirthDay is invalid";
                    }
                }
                return Summary;
            }
        }

        public bool IsValidLastName {
            get
            {
                if (LastName.Length <= 50 && LastName != null) return true;
                else return false;
            }
        }
        

        public bool IsValidBirthDay
        {
            get
            {
                if(BirthDay != null)
                {
                    if (BirthDay < DateTime.Today) return true;
                    else return false;
                }
                else return false;
            }
        }
    }
}
