using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PicDB
{
    class MainWindowViewModel : IMainWindowViewModel
    {
        ISearchViewModel _Search = new SearchViewModel();
        IPictureListViewModel _List;
        BusinessLayer bl = new BusinessLayer();

        //public event PropertyChangedEventHandler PropertyChanged;
        //private IPictureViewModel _pvm;

        public IPictureViewModel CurrentPicture => List.CurrentPicture;

        public IPictureListViewModel List => _List;

        public ISearchViewModel Search => _Search;
        
        public MainWindowViewModel()
        {
            //pvm = new PictureViewModel(new PictureModel("Img1.jpg"));
            bl.Sync();
            _List = new PictureListViewModel(bl.GetPictures(null, null, null, null));
            
        }

        //protected void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}

        //public IPictureViewModel pvm
        //{
        //    get
        //    {
        //        return _pvm;
        //    }

        //    set
        //    {
        //        _pvm = value;
        //        OnPropertyChanged("pvm");
        //    }
        //}
    }
}
