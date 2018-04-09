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
    public class PhotographerViewModel : IPhotographerViewModel, INotifyPropertyChanged
    {
        IPhotographerModel _PhotographerModel;
        int _NumberOfPictures;
        public event PropertyChangedEventHandler PropertyChanged;

        public PhotographerViewModel(IPhotographerModel pm)
        {
            _PhotographerModel = pm;
        }
        public int ID => _PhotographerModel.ID;

        public string FirstName {
            get => _PhotographerModel.FirstName;
            set
            {
                _PhotographerModel.FirstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
            }
        }

        public string LastName {
            get => _PhotographerModel?.LastName;
            set
            {
                _PhotographerModel.LastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
            }
        }

        public DateTime? BirthDay {
            get => _PhotographerModel?.BirthDay;
            set => _PhotographerModel.BirthDay = value;
        }

        public string Notes {
            get => _PhotographerModel?.Notes;
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
                if (!IsValid)
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

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public bool IsValidLastName {
            get
            {
                if(!String.IsNullOrEmpty(LastName))
                {
                    if (LastName.Length <= 50) return true;
                    else return false;
                }
                else
                {
                    return false;
                }
                
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
                else return true;
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

        public PhotographerModel PhotographerModel
        {
            get
            {
                return (PhotographerModel)_PhotographerModel;
            }
        }
    }
}
