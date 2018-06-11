using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    public class PhotographerModel : IPhotographerModel
    {
        int _ID;
        string _Firstname;
        string _Lastname;
        DateTime? _BirthDay;
        string _Notes;

        public PhotographerModel()
        {

        }

        /// <summary>
        /// Database primary key
        /// </summary>
        public int ID {
            get => _ID;
            set => _ID = value;
        }

        /// <summary>
        /// Firstname, including middle name
        /// </summary>
        public string FirstName {
            get => _Firstname;
            set => _Firstname = value;
        }

        /// <summary>
        /// Lastname
        /// </summary>
        public string LastName{
            get => _Lastname;
            set => _Lastname = value;
        }

        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime? BirthDay {
            get => _BirthDay;
            set => _BirthDay = value;
        }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes {
            get => _Notes;
            set => _Notes = value;
        }
    }
}
