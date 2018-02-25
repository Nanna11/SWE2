using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    class CameraListViewModel : ICameraListViewModel
    {
        List<ICameraViewModel> _List = new List<ICameraViewModel>();
        ICameraViewModel _CurrentCamera;

        public IEnumerable<ICameraViewModel> List => _List;

        public ICameraViewModel CurrentCamera => _CurrentCamera;
    }
}
