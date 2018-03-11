using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class PhotorapherModel : IPhotographerModel
    {
        int _ID;
        string _Firstname;
        string _Lastname;
        DateTime? _BirthDay;
        string _Notes;

        public PhotorapherModel()
        {

        }

        public int ID {
            get => _ID;
            set => _ID = value;
        }

        public string FirstName {
            get => _Firstname;
            set => _Firstname = value;
        }

        public string LastName{
            get => _Lastname;
            set => _Lastname = value;
        }

        public DateTime? BirthDay {
            get => _BirthDay;
            set => _BirthDay = value;
        }

        public string Notes {
            get => _Notes;
            set => _Notes = value;
        }
    }
}
