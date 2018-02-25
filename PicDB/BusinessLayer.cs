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
        PictureListViewModel pl = new PictureListViewModel();
        IDataAccessLayer dal = new DataAccessLayer("PicDB", "PicDB", "localhost", "PicDB");

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
            return new EXIFModel();
        }

        public IIPTCModel ExtractIPTC(string filename)
        {
            return new IPTCModel();
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
            pl = new PictureListViewModel();
        }

        public void WriteIPTC(string filename, IIPTCModel iptc)
        {
            throw new NotImplementedException();
        }
    }
}
