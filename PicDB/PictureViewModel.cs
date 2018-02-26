using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class PictureViewModel : IPictureViewModel
    {
        IPictureModel _PictureModel;
        IIPTCViewModel _IPTCViewModel;
        IEXIFViewModel _EXIFViewModel;
        ICameraViewModel _CameraViewModel;
        IPhotographerViewModel _Photographer;

        public PictureViewModel(IPictureModel pm)
        {
            _PictureModel = pm;
            _IPTCViewModel = new IPTCViewModel(_PictureModel.IPTC);
            _EXIFViewModel = new EXIFViewModel(_PictureModel.EXIF);
            _CameraViewModel = new CameraViewModel(_PictureModel.Camera);
        }
        public int ID => _PictureModel.ID;

        public string FileName => _PictureModel.FileName;

        public string FilePath => throw new NotImplementedException();

        public string DisplayName
        {
            get
            {
                string dn = null;

                if(IPTC.Headline != null) dn += IPTC.Headline;
                else dn += FileName;

                dn += " (by ";

                if (Photographer != null)
                {
                    if (Photographer.FirstName != null) dn += Photographer.FirstName + " ";
                    dn += Photographer.LastName;
                }
                else
                {
                    dn += IPTC.ByLine;
                }
                dn += ")";
                return dn;

            }
            
        }

        public IIPTCViewModel IPTC => _IPTCViewModel;

        public IEXIFViewModel EXIF => _EXIFViewModel;

        public IPhotographerViewModel Photographer => _Photographer;

        public ICameraViewModel Camera => _CameraViewModel;
    }
}
