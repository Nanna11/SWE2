﻿using System;
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
    public class UEB2 : IUEB2
    {
        IBusinessLayer bl;
        string path;

        public void HelloWorld()
        {
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return new BusinessLayer();
        }

        public BIF.SWE2.Interfaces.ViewModels.IMainWindowViewModel GetMainWindowViewModel()
        {
            return new MainWindowViewModel(null, null, null);
        }

        public BIF.SWE2.Interfaces.Models.IPictureModel GetPictureModel(string filename)
        {
            return new PictureModel(filename);
        }

        public BIF.SWE2.Interfaces.ViewModels.IPictureViewModel GetPictureViewModel(BIF.SWE2.Interfaces.Models.IPictureModel mdl)
        {
            return new PictureViewModel(mdl);
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

        public ICameraModel GetCameraModel(string producer, string make)
        {
            ICameraModel cm = new CameraModel();
            cm.Producer = producer;
            cm.Make = make;
            return cm;
        }

        public ICameraViewModel GetCameraViewModel(ICameraModel mdl)
        {
            return new CameraViewModel(mdl);
        }
    }
}
