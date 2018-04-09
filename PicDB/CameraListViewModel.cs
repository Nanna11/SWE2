using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    public class CameraListViewModel : ICameraListViewModel
    {
        List<ICameraViewModel> _List;
        int _CurrentIndex = 0;

        public CameraListViewModel(IEnumerable<ICameraViewModel> list)
        {
            _List = list.ToList<ICameraViewModel>();
        }

        public CameraListViewModel(IEnumerable<ICameraModel> list)
        {
            _List = new List<ICameraViewModel>();
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
            }
        }

        public CameraViewModel GetCamera(int ID)
        {
            foreach (ICameraViewModel c in List)
            {
                if (c.ID == ID) return (CameraViewModel)c;
            }
            return null;
        }
    }
}
