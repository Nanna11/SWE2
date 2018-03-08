using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace PicDB
{
    class PictureViewModel : IPictureViewModel
    {
        IPictureModel _PictureModel;
        IIPTCViewModel _IPTCViewModel;
        IEXIFViewModel _EXIFViewModel;
        ICameraViewModel _CameraViewModel;
        IPhotographerViewModel _PhotographerViewModel;

        public PictureViewModel(IPictureModel pm)
        {
            _PictureModel = pm;
            _IPTCViewModel = new IPTCViewModel(_PictureModel.IPTC);
            _EXIFViewModel = new EXIFViewModel(_PictureModel.EXIF);
            _CameraViewModel = new CameraViewModel(_PictureModel.Camera);
            PictureModel p = (PictureModel)_PictureModel;
            _PhotographerViewModel = new PhotographerViewModel(p.Photographer);
        }
        public int ID => _PictureModel.ID;

        public string FileName => _PictureModel.FileName;

        public string FilePath
        {
            get
            {
                string folder;
                try
                {
                    folder = GlobalInformation.Instance.Folder;
                }
                catch (SingletonNotInitializedException)
                {
                    return null;
                }
                string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.Combine(deploypath, folder, FileName);
            }
        }

        public string DisplayName
        {
            get
            {
                string dn = null;

                if (IPTC.Headline != null) dn += IPTC.Headline;
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

        public IPhotographerViewModel Photographer {
            get { return _PhotographerViewModel;}
            set { _PhotographerViewModel = value; }
        }


        public ICameraViewModel Camera
        {
            get { return _CameraViewModel; }
            set { _CameraViewModel = value; }
        }

        public BitmapImage ImageResource
        {
            get
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(FilePath);
                bi.EndInit();
                return bi;
            }
        }
    }
}
