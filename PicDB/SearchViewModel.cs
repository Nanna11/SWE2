using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PicDB
{
    class SearchViewModel : ISearchViewModel, INotifyPropertyChanged
    {
        string _SearchText;
        PictureListViewModel _results;
        public event PropertyChangedEventHandler PropertyChanged;

        public string SearchText {
            get
            {
                if (string.IsNullOrEmpty(_SearchText)) return null;
                else return _SearchText;
            }
            set => _SearchText = value;
        }

        public bool IsActive
        {
            get
            {
                if (string.IsNullOrEmpty(SearchText)) return false;
                else return true;
            }
        }

        public PictureListViewModel Results
        {
            get
            {
                return _results;
            }

            set
            {
                _results = value;
                OnPropertyChanged("Results");
            }
        }

        public int ResultCount
        {
            get
            {
                return _results.Count;
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
