using BIF.SWE2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;
using System.IO;
using System.Reflection;
using BIF.SWE2.Interfaces.ViewModels;

namespace PicDB
{
    public class BusinessLayer : IBusinessLayer
    {
        IOwnDataAccessLayer dal = DBConnectionFactory.Instance.CreateDal("PicDB", "PicDB", "localhost", "PicDB");
        static string _picturepath;
        public delegate void ErrorEventHander(string message);
        public event ErrorEventHander Error;

        public BusinessLayer(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new PathNotSetException("Path was empty");
            string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _picturepath = Path.Combine(deploypath, path);
        }

        public BusinessLayer()
        {
            string folder;
            try
            {
                folder = GlobalInformation.Instance.Folder;
            }
            catch (SingletonNotInitializedException)
            {
                throw new PathNotSetException();
            }
            if (String.IsNullOrEmpty(folder)) throw new PathNotSetException("Path was empty");
            string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _picturepath = Path.Combine(deploypath, folder);
        }

        internal void CurrentPictureChanged(IPictureViewModel currentPicture)
        {
            PictureViewModel pvm = (PictureViewModel)currentPicture;
            Save(pvm.PictureModel);
        }

        internal void CurrentCameraChanged(ICameraViewModel currentCamera)
        {
            CameraViewModel cvm = (CameraViewModel)currentCamera;
            if (!cvm.IsValid)
            {
                Error(cvm.ValidationSummary);
                cvm.UndoUpdate();
                return;
            }
            Save(cvm.CameraModel);
        }

        internal void CurrentPhotographerChanged(IPhotographerViewModel currentPhotographer)
        {
            PhotographerViewModel pvm = (PhotographerViewModel)currentPhotographer;
            if (!pvm.IsValid)
            {
                Error(pvm.ValidationSummary);
                pvm.UndoUpdate();
                return;
            }

            Save(pvm.PhotographerModel);
        }

        public void DeletePhotographer(int ID)
        {
            dal.DeletePhotographer(ID);
        }

        public void DeletePicture(int ID)
        {
            dal.DeletePicture(ID);
        }

        public void DeleteCamera(int ID)
        {
            dal.DeleteCamera(ID);
        }

        public IEXIFModel ExtractEXIF(string filename)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_picturepath);
            if (files.Contains(Path.Combine(_picturepath, filename))) return GetDemoExif();
            else throw new FileNotFoundException();
        }

        public IIPTCModel ExtractIPTC(string filename)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_picturepath);
            if (files.Contains(Path.Combine(_picturepath, filename))) return GetDemoIPTC();
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

        public void Save(ICameraModel camera)
        {
            dal.Save(camera);
        }

        public void Sync()
        {
            IEnumerable<string> tmp = Directory.EnumerateFiles(_picturepath);
            List<string> files = new List<string>();
            foreach (string s in tmp)
            {
                files.Add(Path.GetFileName(s));
            }

            List<IPictureModel> pics = dal.GetPictures(null, null, null, null).ToList();


            foreach (IPictureModel s in pics)
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
        }

        public void WriteIPTC(string filename, IIPTCModel iptc)
        {
            
        }

        private IEXIFModel GetDemoExif()
        {
            EXIFModel e = new EXIFModel();
            e.Make = "Make";
            e.FNumber = 2;
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
