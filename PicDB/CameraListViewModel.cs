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

        public CameraListViewModel(IEnumerable<ICameraViewModel> list)
        {
            _List = new ObservableCollection<ICameraViewModel>();
            foreach(ICameraViewModel c in list)
            {
                _List.Add(c);
            }
        }

        public CameraListViewModel(IEnumerable<ICameraModel> list)
        {
            _List = new ObservableCollection<ICameraViewModel>();
            foreach(ICameraModel c in list)
            {
                _List.Add(new CameraViewModel(c));
            }
        }

        public IEnumerable<ICameraViewModel> List => _List;

        public ICameraViewModel CurrentCamera
        {
            get
            {
                if (CurrentIndex < Count) return List.ElementAt<ICameraViewModel>(CurrentIndex);
                else return null;
            }
        }

        public int Count => List.Count();

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
