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
    public class PhotographerListViewModel : IPhotographerListViewModel, INotifyPropertyChanged
    {
        ObservableCollection<IPhotographerViewModel> _List;
        int _CurrentIndex = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public PhotographerListViewModel(IEnumerable<IPhotographerViewModel> list)
        {
            _List = new ObservableCollection<IPhotographerViewModel>();
            foreach(IPhotographerViewModel p in list)
            {
                _List.Add(p);
            }
        }

        public PhotographerListViewModel(IEnumerable<IPhotographerModel> list)
        {
            _List = new ObservableCollection<IPhotographerViewModel>();
            foreach(IPhotographerModel p in list)
            {
                _List.Add(new PhotographerViewModel(p));
            }
        }

        public IPhotographerViewModel CurrentPhotographer
        {
            get
            {
                if (CurrentIndex < Count) return List.ElementAt<IPhotographerViewModel>(CurrentIndex);
                else return null;
            }
        }

        public PhotographerViewModel GetPhotographer(int ID)
        {
            foreach(IPhotographerViewModel p in List)
            {
                if (p.ID == ID) return (PhotographerViewModel)p;
            }
            return null;
        }

        public void Add(PhotographerModel p)
        {
            if (p == null) return;
            PhotographerViewModel pvm = new PhotographerViewModel(p);
            _List.Add(pvm);
            CurrentIndex = Count - 1;
            OnPropertyChanged("List");
            OnPropertyChanged("Count");
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
                OnPropertyChanged("CurrentPhotographer");
            }
        }

        public IEnumerable<IPhotographerViewModel> List => _List;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
