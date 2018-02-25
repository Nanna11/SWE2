using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class CameraModel : ICameraModel
    {
        int _ID;
        string _Producer;
        string _Make;
        DateTime? _BoughtOn;
        string _Notes;
        decimal _ISOLimitGood = 0;
        decimal _ISOLimitAcceptable = 0;

        public int ID
        {
            get { return _ID; }

            set { _ID = value; }
        }

        public string Producer {
            get { return _Producer; }

            set { _Producer = value; }
        }

        public string Make {
            get { return _Make; }

            set { _Make = value; }
        }

        public DateTime? BoughtOn {
            get { return _BoughtOn; }

            set { _BoughtOn = value; }
        }

        public string Notes {
            get { return _Notes; }

            set { _Notes = value; }
        }

        public decimal ISOLimitGood {
            get { return _ISOLimitGood; }

            set {
                _ISOLimitGood = value;
            }
        }

        public decimal ISOLimitAcceptable {
            get { return _ISOLimitAcceptable; }

            set { _ISOLimitAcceptable = value; }
        }
    }
}
