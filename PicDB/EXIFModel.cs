using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    /// <summary>
    /// model for exif data of a picture
    /// </summary>
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

        /// <summary>
        /// Name of camera
        /// </summary>
        public string Make {
            get => _Make;
            set => _Make = value;
        }

        /// <summary>
        /// Aperture number
        /// </summary>
        public decimal FNumber {
            get => _FNumber;
            set => _FNumber = value;
        }

        /// <summary>
        /// Exposure time
        /// </summary>
        public decimal ExposureTime {
            get => _ExposureTime;
            set => _ExposureTime = value;
        }

        /// <summary>
        /// ISO value
        /// </summary>
        public decimal ISOValue {
            get => _ISOValue;
            set => _ISOValue = value;
        }

        /// <summary>
        /// Flash yes/no
        /// </summary>
        public bool Flash {
            get => _Flash;
            set => _Flash = value;
        }

        /// <summary>
        /// The exposure program
        /// </summary>
        public ExposurePrograms ExposureProgram {
            get => _ExposureProgram;
            set => _ExposureProgram = value;
        }
    }
}
