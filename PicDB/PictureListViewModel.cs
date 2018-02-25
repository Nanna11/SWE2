using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    class PictureListViewModel : IPictureListViewModel
    {
        int _CurrentIndex;

        List<IPictureViewModel> _List = new List<IPictureViewModel>();

        public IPictureViewModel CurrentPicture => List.ElementAt<IPictureViewModel>(CurrentIndex - 1);

        public IEnumerable<IPictureViewModel> List => _List;

        public IEnumerable<IPictureViewModel> PrevPictures
        {
            get
            {
                List<IPictureViewModel> PrevPic = new List<IPictureViewModel>();
                for(int i = 0; i < CurrentIndex - 1; i++)
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
                for (int i = CurrentIndex - 1; i < Count; i++)
                {
                    NextPic.Add(List.ElementAt<IPictureViewModel>(i));
                }
                return NextPic;
            }
        }

        public int Count => List.Count<IPictureViewModel>();

        public int CurrentIndex => _CurrentIndex;

        public string CurrentPictureAsString => CurrentPicture.DisplayName;
    }
}
