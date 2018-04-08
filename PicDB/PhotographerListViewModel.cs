using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    public class PhotographerListViewModel : IPhotographerListViewModel
    {
        List<IPhotographerViewModel> _List = new List<IPhotographerViewModel>();
        IPhotographerViewModel _CurrentPhotographer;

        public IEnumerable<IPhotographerViewModel> List => _List;

        public IPhotographerViewModel CurrentPhotographer => _CurrentPhotographer;
    }
}
