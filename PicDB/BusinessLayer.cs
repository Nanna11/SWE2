using BIF.SWE2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class BusinessLayer : IBusinessLayer
    {
        PictureListViewModel pl;
        IDataAccessLayer dal = new DataAccessLayer("PicDB", "PicDB", "localhost", "PicDB");
        string _picturepath;

        public BusinessLayer(string path)
        {
            _picturepath = path;
            Sync();
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
            return GetDemoExif();
        }

        public IIPTCModel ExtractIPTC(string filename)
        {
            return GetDemoIPTC();
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
            throw new NotImplementedException();
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
