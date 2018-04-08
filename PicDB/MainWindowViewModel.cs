using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PicDB
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        SearchViewModel _Search;
        IPictureListViewModel _List;
        BusinessLayer bl = new BusinessLayer();

        public event PropertyChangedEventHandler PropertyChanged;

        public IPictureViewModel CurrentPicture
        {
            get
            {
                return List.CurrentPicture;
            }
        }

        public int CurrentIndex
        {
            get
            {
                return List.CurrentIndex;
            }

            set
            {
                ((PictureListViewModel)List).CurrentIndex = value;
                OnPropertyChanged("CurrentPicture");
                OnPropertyChanged("CurrentIndex");
            }
        }

        public IPictureListViewModel List
        {
            get
            {
                return _List;
            }

            set
            {
                _List = value;
                OnPropertyChanged("List");
                OnPropertyChanged("CurrentPicture");
            }
        }

        public ISearchViewModel Search => _Search;
        
        public MainWindowViewModel()
        {
            bl.Sync();
            List = new PictureListViewModel(bl.GetPictures(null, null, null, null));
            _Search = new SearchViewModel();
            _Search.SearchActivated += SearchPictures;

        }

        internal void CurrentPictureChanged()
        {
            bl.CurrentPictureChanged(CurrentPicture);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void SearchPictures(object sender, SearchEventArgs e)
        {
            List = new PictureListViewModel(bl.GetPictures(e.Searchtext, null, null, null));
        }

    }
}
