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
        IOwnDataAccessLayer dal = DBConnectionFactory.CreateDal("PicDB", "PicDB", "localhost", "PicDB");
        static string _picturepath;
        public delegate void ErrorEventHander(string message);
        public event ErrorEventHander Error;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="path">path from execution folder to pictures that should be managed</param>
        public BusinessLayer(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new PathNotSetException("Path was empty");
            string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _picturepath = Path.Combine(deploypath, path);
        }

        /// <summary>
        /// constructor taking picture path from global config
        /// </summary>
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

        /// <summary>
        /// save picture changes to database
        /// </summary>
        /// <param name="currentPicture"></param>
        internal void CurrentPictureChanged(IPictureViewModel currentPicture)
        {
            PictureViewModel pvm = (PictureViewModel)currentPicture;
            Save(pvm.PictureModel);
        }

        /// <summary>
        /// save camera changes to database
        /// check if model is valid and undo updates if not valid
        /// </summary>
        /// <param name="currentCamera"></param>
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

        /// <summary>
        /// save photographer changes to database
        /// check if model is valid and undo updates if not valid
        /// </summary>
        /// <param name="currentPhotographer"></param>
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

        /// <summary>
        /// delete photographer with given ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeletePhotographer(int ID)
        {
            dal.DeletePhotographer(ID);
        }

        /// <summary>
        /// delete picture with given ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeletePicture(int ID)
        {
            dal.DeletePicture(ID);
        }

        /// <summary>
        /// delete camera with given ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteCamera(int ID)
        {
            dal.DeleteCamera(ID);
        }

        /// <summary>
        /// gets demo exif data if picture is present in folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IEXIFModel ExtractEXIF(string filename)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_picturepath);
            if (files.Contains(Path.Combine(_picturepath, filename))) return GetDemoExif();
            else throw new FileNotFoundException();
        }

        /// <summary>
        /// gets demo iptc data if picture is present in folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IIPTCModel ExtractIPTC(string filename)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_picturepath);
            if (files.Contains(Path.Combine(_picturepath, filename))) return GetDemoIPTC();
            else throw new FileNotFoundException();
        }

        /// <summary>
        /// returns camera with given ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ICameraModel GetCamera(int ID)
        {
            return dal.GetCamera(ID);
        }

        /// <summary>
        /// gets all cameras
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICameraModel> GetCameras()
        {
            return dal.GetCameras();
        }

        /// <summary>
        /// gets photographer with given ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IPhotographerModel GetPhotographer(int ID)
        {
            return dal.GetPhotographer(ID);
        }

        /// <summary>
        /// gets all photographers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPhotographerModel> GetPhotographers()
        {
            return dal.GetPhotographers();
        }

        /// <summary>
        /// gets picture with given ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IPictureModel GetPicture(int ID)
        {
            return dal.GetPicture(ID);
        }

        /// <summary>
        /// gets all pictures
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPictureModel> GetPictures()
        {
            return dal.GetPictures(null, null, null, null);
        }

        /// <summary>
        /// gets filtered pictures
        /// </summary>
        /// <param name="namePart"></param>
        /// <param name="photographerParts"></param>
        /// <param name="iptcParts"></param>
        /// <param name="exifParts"></param>
        /// <returns></returns>
        public IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts)
        {
            return dal.GetPictures(namePart, photographerParts, iptcParts, exifParts);
        }

        /// <summary>
        /// save a picture to database
        /// </summary>
        /// <param name="picture"></param>
        public void Save(IPictureModel picture)
        {
            dal.Save(picture);
        }

        /// <summary>
        /// save a photographer to database
        /// </summary>
        /// <param name="photographer"></param>
        public void Save(IPhotographerModel photographer)
        {
            dal.Save(photographer);
        }

        /// <summary>
        /// save a camera to database
        /// </summary>
        /// <param name="camera"></param>
        public void Save(ICameraModel camera)
        {
            dal.Save(camera);
        }

        /// <summary>
        /// sync database with folder
        /// </summary>
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

        /// <summary>
        /// change IPTC data in file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="iptc"></param>
        public void WriteIPTC(string filename, IIPTCModel iptc)
        {
            
        }

        /// <summary>
        /// creates demo exif data
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// creates demo IPTC data
        /// </summary>
        /// <returns></returns>
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
