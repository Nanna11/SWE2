using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PicDB
{
    public class CameraListViewModel : ICameraListViewModel, INotifyPropertyChanged
    {
        ObservableCollection<ICameraViewModel> _List;
        int _CurrentIndex = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// creates camera listviewmodel containing picures in given enumerable
        /// </summary>
        /// <param name="list"></param>
        public CameraListViewModel(IEnumerable<ICameraViewModel> list)
        {
            _List = new ObservableCollection<ICameraViewModel>();
            foreach(ICameraViewModel c in list)
            {
                _List.Add(c);
            }
        }

        /// <summary>
        /// creates camera listviewmodel containing picures in given enumerable
        /// </summary>
        /// <param name="list"></param>
        public CameraListViewModel(IEnumerable<ICameraModel> list)
        {
            _List = new ObservableCollection<ICameraViewModel>();
            foreach(ICameraModel c in list)
            {
                _List.Add(new CameraViewModel(c));
            }
        }

        /// <summary>
        /// returns list of pictures in picturelistviewmodel
        /// </summary>
        public IEnumerable<ICameraViewModel> List => _List;

        /// <summary>
        /// returns currently selected Camera
        /// </summary>
        public ICameraViewModel CurrentCamera
        {
            get
            {
                if (CurrentIndex < Count) return List.ElementAt<ICameraViewModel>(CurrentIndex);
                else return null;
            }
        }

        /// <summary>
        /// returns number of pictures contained in cameralistviewmodel
        /// </summary>
        public int Count => List.Count();

        /// <summary>
        /// gets and sets the index of currently selected picture
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
                if (_CurrentIndex >= _List.Count) _CurrentIndex = 0;
                OnPropertyChanged("CurrentIndex");
                OnPropertyChanged("CurrentCamera");
            }
        }

        public void Add(CameraModel p)
        {
            if (p == null) return;
            CameraViewModel pvm = new CameraViewModel(p);
            _List.Add(pvm);
            CurrentIndex = Count - 1;
            OnPropertyChanged("List");
            OnPropertyChanged("Count");
        }

        public CameraViewModel GetCamera(int ID)
        {
            foreach (ICameraViewModel c in List)
            {
                if (c.ID == ID) return (CameraViewModel)c;
            }
            return null;
        }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
