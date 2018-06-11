using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;
using BIF.SWE2.Interfaces.ViewModels;
using PicDB;

namespace Uebungen
{
    public class UEB5 : IUEB5
    {
        string path;

        public void HelloWorld()
        {
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return new BusinessLayer();
        }

        public void TestSetup(string picturePath)
        {
            Directory.CreateDirectory(picturePath);
            DBConnectionFactory.Mock = true;
            try
            {
                GlobalInformation gi = GlobalInformation.InitializeInstance(picturePath);
            }
            catch (Exception) { }
        }

        public IPhotographerModel GetEmptyPhotographerModel()
        {
            return new PhotographerModel();
        }

        public IPhotographerViewModel GetPhotographerViewModel(IPhotographerModel mdl)
        {
            return new PhotographerViewModel(mdl);
        }

        public ICameraModel GetEmptyCameraModel()
        {
            return new CameraModel();
        }

        public ICameraViewModel GetCameraViewModel(ICameraModel mdl)
        {
            return new CameraViewModel(mdl);
        }
    }
}
