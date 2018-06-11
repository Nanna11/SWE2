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
        PictureViewModel _PictureViewModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public EXIFViewModel(IEXIFModel em, PictureViewModel pvm)
        {
            _EXIFModel = em;
            _PictureViewModel = pvm;
            pvm.PropertyChanged += SubPropertyChanged;
        }

        // <summary>
        /// Name of camera
        /// </summary>
        public string Make => _EXIFModel.Make;

        /// <summary>
        /// Aperture number
        /// </summary>
        public decimal FNumber => _EXIFModel.FNumber;

        /// <summary>
        /// Exposure time
        /// </summary>
        public decimal ExposureTime => _EXIFModel.ExposureTime;

        /// <summary>
        /// ISO value
        /// </summary>
        public decimal ISOValue => _EXIFModel.ISOValue;

        /// <summary>
        /// Flash yes/no
        /// </summary>
        public bool Flash => _EXIFModel.Flash;

        /// <summary>
        /// The Exposure Program as string
        /// </summary>
        public string ExposureProgram => _EXIFModel.ExposureProgram.ToString();

        /// <summary>
        /// The Exposure Program as image resource
        /// </summary>
        public string ExposureProgramResource
        {
            get
            {
                return ExposureProgram.ToString();
            }
        }

        /// <summary>
        /// Gets or sets a optional camera view model
        /// </summary>
        public ICameraViewModel Camera {
            get => _PictureViewModel.Camera;
            set
            {
                ((PictureViewModel)_PictureViewModel).Camera = value;
                OnPropertyChanged("Camera");
                OnPropertyChanged("ISORating");
            }
        }

        /// <summary>
        /// Returns a ISO rating if a camera is set.
        /// </summary>
        public ISORatings ISORating
        {
            get
            {
                if (Camera != null)
                {
                    return Camera.TranslateISORating(ISOValue);
                }
                else return ISORatings.NotDefined;
            }
        }

        /// <summary>
        /// Returns a image resource of a ISO rating if a camera is set.
        /// </summary>
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
                case "Camera":
                    if(sender == _PictureViewModel)
                    {
                        OnPropertyChanged("Camera");
                        OnPropertyChanged("ISORatingResource");
                        OnPropertyChanged("ISORating");
                    }
                    break;
            }
        }
    }
}
