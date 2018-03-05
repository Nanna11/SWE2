using BIF.SWE2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using BIF.SWE2.Interfaces.ViewModels;

namespace PicDB
{
    class MockBusinessLayer : IBusinessLayer
    {
        PictureListViewModel pl = new PictureListViewModel(new List<IPictureModel>());
        IDataAccessLayer dal = new MockDataAccessLayer();
        string _path;

        public MockBusinessLayer(string path)
        {
            _path = "Pictures";
            string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _path = Path.Combine(deploypath, _path);
        }

        public void DeletePhotographer(int ID)
        {
            dal.DeletePhotographer(ID);
        }

        public void DeletePicture(int ID)
        {
            dal.DeletePicture(ID);
        }

        public IEXIFModel ExtractEXIF(string filename)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_path);
            if (files.Contains(Path.Combine(_path, filename))) return GetDemoExif();
            else throw new FileNotFoundException();
        }

        public IIPTCModel ExtractIPTC(string filename)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_path);
            if (files.Contains(Path.Combine(_path, filename))) return GetDemoIPTC();
            else throw new FileNotFoundException();
        }

        public ICameraModel GetCamera(int ID)
        {
            return dal.GetCamera(ID);
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            return dal.GetCameras();
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            return dal.GetPhotographer(ID);
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            return dal.GetPhotographers();
        }

        public IPictureModel GetPicture(int ID)
        {
            return dal.GetPicture(ID);
        }

        public IEnumerable<IPictureModel> GetPictures()
        {
            return dal.GetPictures(null, null, null, null);
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            return dal.GetPictures(namePart, photographerParts, iptcParts, exifParts);
        }

        public void Save(IPictureModel picture)
        {
            dal.Save(picture);
        }

        public void Save(IPhotographerModel photographer)
        {
            dal.Save(photographer);
        }

        public void Sync()
        {
            IEnumerable<string> tmp = Directory.EnumerateFiles(_path);
            List<string> files = new List<string>();
            foreach (string s in tmp)
            {
                files.Add(Path.GetFileName(s));
            }

            List<IPictureModel> pics = dal.GetPictures(null, null, null, null).ToList();
  

            foreach(IPictureModel s in pics)
            {
                if (!files.Contains<string>(s.FileName))
                {
                    DeletePicture(s.ID);
                }
                else
                {
                    files.Remove(s.FileName);
                }
            }

            foreach (string s in files)
            {
                IPictureModel pm = new PictureModel(s);
                pm.EXIF = ExtractEXIF(s);
                pm.IPTC = ExtractIPTC(s);
                Save(pm);               
            }

            pl = new PictureListViewModel(dal.GetPictures(null, null, null, null));
        }

        public void WriteIPTC(string filename, IIPTCModel iptc)
        {
            throw new NotImplementedException();
        }

        private IEXIFModel GetDemoExif()
        {
            EXIFModel e = new EXIFModel();
            e.Make = "Make";
            e.FNumber = 1;
            e.ExposureTime = 5;
            e.ISOValue = 200;
            e.Flash = true;
            e.ExposureProgram = ExposurePrograms.LandscapeMode;
            return e;
        }

        private IIPTCModel GetDemoIPTC()
        {
            IIPTCModel i = new IPTCModel();
            i.ByLine = "ByLine";
            i.Caption = "Captoin";
            i.CopyrightNotice = "CopyrightNotice";
            i.Headline = "Headline";
            i.Keywords = "Keywords";
            return i;
        }
    }
}
