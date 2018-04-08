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
    public class EXIFViewModel : IEXIFViewModel, INotifyPropertyChanged
    {
        IEXIFModel _EXIFModel;
        ICameraViewModel _CameraViewModel;
        public event PropertyChangedEventHandler PropertyChanged;

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
            set
            {
                _CameraViewModel = value;
                ((CameraViewModel)_CameraViewModel).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
                OnPropertyChanged("Camera");
                OnPropertyChanged("ISORating");
            }
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

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SubPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ISOLimitGood":
                    OnPropertyChanged("ISORating");
                    break;
                case "ISOLimitAcceptable":
                    OnPropertyChanged("ISORating");
                    break;
            }
        }
    }
}
