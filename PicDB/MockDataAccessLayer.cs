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
    public class MockDataAccessLayer : IOwnDataAccessLayer
    {
        List<IPictureModel> picl = new List<IPictureModel>();
        List<IPhotographerModel> phol = new List<IPhotographerModel>();
        List<ICameraModel> caml = new List<ICameraModel>();

        public MockDataAccessLayer()
        {
            IPictureModel p = new PictureModel("Blume.jpg");
            p.ID = 1234;
            Save(p);
            IPhotographerModel ph = new PhotographerModel();
            ph.ID = 1234;
            Save(ph);
            ICameraModel c = new CameraModel();
            c.ID = 1234;
            Save(c);
        }

        /// <summary>
        /// Deletes a Photographer. A Exception is thrown if a Photographer is still linked to a picture.
        /// </summary>
        /// <param name="ID"></param>
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

        /// <summary>
        /// Deletes a Picture from the database.
        /// </summary>
        /// <param name="ID"></param>
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

        /// <summary>
        /// deletes camera from database
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteCamera(int ID)
        {
            List<ICameraModel> del = new List<ICameraModel>();
            foreach (ICameraModel p in caml)
            {
                if (p.ID == ID) del.Add(p);
            }

            foreach (ICameraModel p in del)
            {
                caml.Remove(p);
            }
        }

        /// <summary>
        /// Returns ONE Camera
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ICameraModel GetCamera(int ID)
        {
            foreach(ICameraModel c in caml)
            {
                if (c.ID == ID) return c;
            }
            throw new Exception();
        }

        /// <summary>
        /// Returns a list of ALL Cameras.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICameraModel> GetCameras()
        {
            return caml;
        }

        /// <summary>
        /// Returns ONE Photographer
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IPhotographerModel GetPhotographer(int ID)
        {
            foreach (IPhotographerModel c in phol)
            {
                if (c.ID == ID) return c;
            }
            throw new Exception();
        }

        /// <summary>
        /// Returns a list of ALL Photographers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            return phol;
        }

        /// <summary>
        /// Returns ONE Picture from the database.
        /// </summary>
        /// <returns></returns>
        public IPictureModel GetPicture(int ID)
        {
            foreach (IPictureModel c in picl)
            {
                if (c.ID == ID) return c;
            }
            throw new Exception();
        }

        /// <summary>
        /// Returns a filterd list of Pictures from the directory, based on a database query.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Saves all changes to the database.
        /// </summary>
        /// <param name="picture"></param>
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

        /// <summary>
        /// Saves all changes.
        /// </summary>
        /// <param name="photographer"></param>
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

        /// <summary>
        /// saves camera to database
        /// </summary>
        /// <param name="camera"></param>
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
