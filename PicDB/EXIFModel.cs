using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    public class EXIFModel : IEXIFModel
    {
        string _Make;
        decimal _FNumber;
        decimal _ExposureTime;
        decimal _ISOValue;
        bool _Flash;
        ExposurePrograms _ExposureProgram;

        public EXIFModel()
        {
        }

        public string Make {
            get => _Make;
            set => _Make = value;
        }

        public decimal FNumber {
            get => _FNumber;
            set => _FNumber = value;
        }

        public decimal ExposureTime {
            get => _ExposureTime;
            set => _ExposureTime = value;
        }

        public decimal ISOValue {
            get => _ISOValue;
            set => _ISOValue = value;
        }

        public bool Flash {
            get => _Flash;
            set => _Flash = value;
        }

        public ExposurePrograms ExposureProgram {
            get => _ExposureProgram;
            set => _ExposureProgram = value;
        }
    }
}
