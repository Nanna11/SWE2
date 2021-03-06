﻿using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace PicDB
{
    public class PictureListViewModel : IPictureListViewModel, INotifyPropertyChanged
    {
        int _CurrentIndex = 0;
        ObservableCollection<IPictureViewModel> _List;
        public event PropertyChangedEventHandler PropertyChanged;

        public PictureListViewModel(IEnumerable<IPictureViewModel> p)
        {
            _List = new ObservableCollection<IPictureViewModel>();
            foreach(IPictureViewModel pic in List)
            {
                _List.Add(pic);
            }
        }

        public PictureListViewModel(IEnumerable<IPictureModel> p)
        {
            _List = new ObservableCollection<IPictureViewModel>();
            foreach(PictureModel pic in p)
            {
                PictureViewModel v = new PictureViewModel(pic);
                _List.Add(v);
            }
        }

        /// <summary>
        /// ViewModel of the current picture
        /// </summary>
        public IPictureViewModel CurrentPicture
        {
            get
            {
                if (CurrentIndex < Count) return List.ElementAt<IPictureViewModel>(CurrentIndex);
                else return null;
            }
        }

        /// <summary>
        /// List of all PictureViewModels
        /// </summary>
        public IEnumerable<IPictureViewModel> List => _List;


        /// <summary>
        /// All prev. pictures to the current selected picture.
        /// </summary>
        public IEnumerable<IPictureViewModel> PrevPictures
        {
            get
            {
                List<IPictureViewModel> PrevPic = new List<IPictureViewModel>();
                for(int i = 0; i < CurrentIndex; i++)
                {
                    PrevPic.Add(List.ElementAt<IPictureViewModel>(i));
                }
                return PrevPic;
            }
        }

        /// <summary>
        /// All next pictures to the current selected picture.
        /// </summary>
        public IEnumerable<IPictureViewModel> NextPictures
        {
            get
            {
                List<IPictureViewModel> NextPic = new List<IPictureViewModel>();
                for (int i = CurrentIndex + 1; i < Count; i++)
                {
                    NextPic.Add(List.ElementAt<IPictureViewModel>(i));
                }
                return NextPic;
            }
        }

        /// <summary>
        /// Number of all images
        /// </summary>
        public int Count => List.Count<IPictureViewModel>();

        /// <summary>
        /// The current Index, 1 based
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                return _CurrentIndex;
            }

            set
            {
                _CurrentIndex = value;
                if (_CurrentIndex < 0) _CurrentIndex = 0;
                if (_CurrentIndex >=  _List.Count) _CurrentIndex = 0;
                OnPropertyChanged("CurrentIndex");
                OnPropertyChanged("CurrentPicture");
            }
        }

        /// <summary>
        /// {CurrentIndex} of {Cout}
        /// </summary>
        public string CurrentPictureAsString
        {
            get
            {
                return (CurrentIndex + 1).ToString() + " out of " + Count.ToString();
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

        public void SetPhotographers(PhotographerListViewModel p)
        {
            foreach(IPictureViewModel picture in List)
            {
                if(picture.Photographer != null)
                {
                    ((PictureViewModel)picture).Photographer = p.GetPhotographer(picture.Photographer.ID);
                }
            }
        }

        public void SetCameras(CameraListViewModel c)
        {
            foreach (IPictureViewModel picture in List)
            {
                if (picture.Camera != null)
                {
                    ((PictureViewModel)picture).Camera = c.GetCamera(picture.Camera.ID);
                }
            }
        }
    }
}
