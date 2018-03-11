using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PicDB
{
    class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        ISearchViewModel _Search = new SearchViewModel();
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


        public IPictureListViewModel List => _List;

        public ISearchViewModel Search => _Search;
        
        public MainWindowViewModel()
        {
            bl.Sync();
            _List = new PictureListViewModel(bl.GetPictures(null, null, null, null));
            ((PictureListViewModel)_List).PropertyChanged += new PropertyChangedEventHandler(SubPropertyChanged);
            
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

        private void SubPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            Trace.WriteLine(e.PropertyName);
        }

        public void ChangeIndex()
        {
            ((PictureListViewModel)_List).CurrentIndex = 1;
            OnPropertyChanged("CurrentPicture");
        }
    }
}
