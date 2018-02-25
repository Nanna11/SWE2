using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    class MainWindowViewModel : IMainWindowViewModel
    {
        ISearchViewModel _Search = new SearchViewModel();
        IPictureListViewModel _List = new PictureListViewModel();

        public IPictureViewModel CurrentPicture => List.CurrentPicture;

        public IPictureListViewModel List => _List;

        public ISearchViewModel Search => _Search;
    }
}
