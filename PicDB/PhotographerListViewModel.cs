using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    public class PhotographerListViewModel : IPhotographerListViewModel
    {
        List<IPhotographerViewModel> _List;
        int _CurrentIndex = 0;

        public PhotographerListViewModel(IEnumerable<IPhotographerViewModel> list)
        {
            _List = list.ToList<IPhotographerViewModel>();
        }

        public PhotographerListViewModel(IEnumerable<IPhotographerModel> list)
        {
            _List = new List<IPhotographerViewModel>();
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
            }
        }

        public IEnumerable<IPhotographerViewModel> List => _List;
    }
}
