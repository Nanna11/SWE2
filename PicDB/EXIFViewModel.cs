using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class EXIFViewModel : IEXIFViewModel
    {
        IEXIFModel _EXIFModel;
        ICameraViewModel _CameraViewModel;

        public EXIFViewModel(IEXIFModel em)
        {
            _EXIFModel = em;
        }

        public string Make => _EXIFModel.Make;

        public decimal FNumber => _EXIFModel.FNumber;

        public decimal ExposureTime => _EXIFModel.ExposureTime;

        public decimal ISOValue => _EXIFModel.ISOValue;

        public bool Flash => _EXIFModel.Flash;

        public string ExposureProgram => _EXIFModel.ExposureProgram.ToString();

        public string ExposureProgramResource
        {
            get
            {
                return ExposureProgram.ToString();
            }
        }

        public ICameraViewModel Camera {
            get => _CameraViewModel;
            set => _CameraViewModel = value;
        }

        public ISORatings ISORating
        {
            get
            {
                if (_CameraViewModel != null)
                {
                    return _CameraViewModel.TranslateISORating(ISOValue);
                }
                else return ISORatings.NotDefined;
            }
        }

        public string ISORatingResource
        {
            get
            {
                return ISORating.ToString();
            }
        }
    }
}
