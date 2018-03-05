using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using System.Data.SqlClient;
using System.Data;

namespace PicDB
{
    class MockDataAccessLayer : IDataAccessLayer
    {
        List<IPictureModel> picl = new List<IPictureModel>();
        List<IPhotographerModel> phol = new List<IPhotographerModel>();
        List<ICameraModel> caml = new List<ICameraModel>();

        public MockDataAccessLayer()
        {
            IPictureModel p = new PictureModel("ASonne.jpg");
            Save(p);
            p = new PictureModel("Blume.jpg");
            Save(p);
            Save(new PhotorapherModel());
            Save(new PhotorapherModel());
            Save(new CameraModel());
        }

        public void DeletePhotographer(int ID)
        {
            List<IPhotographerModel> del = new List<IPhotographerModel>();
            foreach(IPhotographerModel p in phol)
            {
                if (p.ID == ID) del.Add(p);
            }

            foreach(IPhotographerModel p in del)
            {
                phol.Remove(p);
            }
        }

        public void DeletePicture(int ID)
        {
            List<IPictureModel> del = new List<IPictureModel>();
            foreach (IPictureModel p in picl)
            {
                if (p.ID == ID) del.Add(p);
            }

            foreach (IPictureModel p in del)
            {
                picl.Remove(p);
            }
        }

        public void DeleteCamera(int ID)
        {
            List<ICameraModel> del = new List<ICameraModel>();
            foreach (ICameraModel p in picl)
            {
                if (p.ID == ID) del.Add(p);
            }

            foreach (ICameraModel p in del)
            {
                caml.Remove(p);
            }
        }

        public ICameraModel GetCamera(int ID)
        {
            return new CameraModel();
        }

        public IEnumerable<ICameraModel> GetCameras()
        {
            return caml;
        }

        public IPhotographerModel GetPhotographer(int ID)
        {
            return new PhotorapherModel();
        }

        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            return phol;
        }

        public IPictureModel GetPicture(int ID)
        {
            return new PictureModel("test.jpg");
        }

        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, BIF.SWE2.Interfaces.Models.IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            if(namePart != null)
            {
                IPictureModel pi = new PictureModel("Blume.jpg");
                foreach(IPictureModel pm in picl)
                {
                    if (pm.FileName == pi.FileName) pi.ID = pm.ID;
                }
                Save(pi);
            }
            
            if (!string.IsNullOrEmpty(namePart))
            {
                List<IPictureModel> result = new List<IPictureModel>();
                foreach(IPictureModel p in picl)
                {
                    if (p.FileName.ToLower().Contains(namePart.ToLower())) result.Add(p);
                }
                return result;
            }
            else
            {
                return picl;
            }
            
        }

        public void Save(IPictureModel picture)
        {
            int max = 0;
            IPictureModel del = null;

            if(picture.ID > 0)
            {
                foreach (IPictureModel p in picl)
                {
                    if (p.ID == picture.ID)
                    {
                        del = p;
                        break;
                    }
                }
                picl.Remove(del); //Add errorhandling here
            }
            else
            {
                foreach (IPictureModel p in picl)
                {
                    if (p.ID > max) max = p.ID;
                }
                picture.ID = max + 1;
            }
            picl.Add(picture);
        }

        public void Save(IPhotographerModel photographer)
        {
            int max = 0;
            IPhotographerModel del = null;

            if (photographer.ID > 0)
            {
                foreach (IPhotographerModel p in phol)
                {
                    if (p.ID == photographer.ID)
                    {
                        del = p;
                        break;
                    }
                }
                phol.Remove(del); //Add errorhandling here
            }
            else
            {
                foreach (IPhotographerModel p in phol)
                {
                    if (p.ID > max) max = p.ID;
                }
                photographer.ID = max + 1;
            }
            phol.Add(photographer);
        }

        public void Save(ICameraModel camera)
        {
            int max = 0;
            ICameraModel del = null;

            if (camera.ID > 0)
            {
                foreach (ICameraModel p in caml)
                {
                    if (p.ID == camera.ID)
                    {
                        del = p;
                        break;
                    }
                }
                caml.Remove(del); //Add errorhandling here
            }
            else
            {
                foreach (ICameraModel p in caml)
                {
                    if (p.ID > max) max = p.ID;
                }
                camera.ID = max + 1;
            }
            caml.Add(camera);
        }
    }
}
