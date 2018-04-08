using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
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
        List<IPictureViewModel> _List;
        public event PropertyChangedEventHandler PropertyChanged;

        public PictureListViewModel(IEnumerable<IPictureViewModel> p)
        {
            _List = p.ToList<IPictureViewModel>();
        }

        public PictureListViewModel(IEnumerable<IPictureModel> p)
        {
            _List = new List<IPictureViewModel>();
            foreach(PictureModel pic in p)
            {
                PictureViewModel v = new PictureViewModel(pic);
                _List.Add(v);
            }
        }

        public IPictureViewModel CurrentPicture
        {
            get
            {
                if (CurrentIndex < Count) return List.ElementAt<IPictureViewModel>(CurrentIndex);
                else return null;
            }
        }

        public IEnumerable<IPictureViewModel> List => _List;

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

        public int Count => List.Count<IPictureViewModel>();

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
    }
}
