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
        DateTime? _LastBirthday= null;
        string _LastLastName = null;

        public PhotographerViewModel(IPhotographerModel pm)
        {
            _PhotographerModel = pm;
        }

        /// <summary>
        /// Database primary key
        /// </summary>
        public int ID => _PhotographerModel.ID;

        /// <summary>
        /// Firstname, including middle name
        /// </summary>
        public string FirstName {
            get => _PhotographerModel.FirstName;
            set
            {
                _PhotographerModel.FirstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary>
        /// Lastname
        /// </summary>
        public string LastName {
            get => _PhotographerModel?.LastName;
            set
            {
                _LastLastName = LastName;
                _PhotographerModel.LastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("FullName");
            }
        }

        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime? BirthDay {
            get => _PhotographerModel?.BirthDay;
            set
            {
                _LastBirthday = BirthDay;
                _PhotographerModel.BirthDay = value;
                OnPropertyChanged("BirthDay");
            }
        }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes {
            get => _PhotographerModel?.Notes;
            set => _PhotographerModel.Notes = value;
        }

        /// <summary>
        /// Returns the number of Pictures
        /// </summary>
        public int NumberOfPictures => _NumberOfPictures;

        /// <summary>
        /// Returns true, if the model is valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (IsValidBirthDay && IsValidLastName) return true;
                else return false;
            }
        }

        /// <summary>
        /// Returns a summary of validation errors
        /// </summary>
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

        /// <summary>
        /// returns full name of photographer
        /// </summary>
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        /// <summary>
        /// returns true if the last name is valid
        /// </summary>
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

        /// <summary>
        /// returns true if the birthday is valid
        /// </summary>
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

        /// <summary>
        /// returns photographer model
        /// </summary>
        public PhotographerModel PhotographerModel
        {
            get
            {
                return (PhotographerModel)_PhotographerModel;
            }
        }

        /// <summary>
        /// resumes last valid state
        /// </summary>
        public void UndoUpdate()
        {
            if(!IsValidBirthDay) BirthDay = _LastBirthday;
            if(!IsValidLastName) LastName = _LastLastName;
        }
    }
}
