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
    public class UEB1 : IUEB1
    {
        string path;

        public IApplication GetApplication()
        {
            return new App();
        }

        public void HelloWorld()
        {
            // I'm fine
        }

        public IDataAccessLayer GetAnyDataAccessLayer()
        {
            return DBConnectionFactory.CreateDal("PicDB", "PicDB", "localhost", "PicDB");
        }

        public IBusinessLayer GetBusinessLayer()
        {
            return new BusinessLayer();
        }

        public BIF.SWE2.Interfaces.Models.IEXIFModel GetEmptyEXIFModel()
        {
            return new EXIFModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IEXIFViewModel GetEmptyEXIFViewModel()
        {
            return new EXIFViewModel(new EXIFModel(), new PictureViewModel(new PictureModel("test")));
        }

        public BIF.SWE2.Interfaces.Models.IIPTCModel GetEmptyIPTCModel()
        {
            return new IPTCModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IIPTCViewModel GetEmptyIPTCViewModel()
        {
            return new IPTCViewModel(new IPTCModel());
        }

        public BIF.SWE2.Interfaces.ViewModels.IMainWindowViewModel GetEmptyMainWindowViewModel()
        {
            return new MainWindowViewModel(null, null, null);
        }

        public BIF.SWE2.Interfaces.ViewModels.IPhotographerListViewModel GetEmptyPhotographerListViewModel()
        {
            return new PhotographerListViewModel(new List<IPhotographerViewModel>());
        }

        public BIF.SWE2.Interfaces.Models.IPhotographerModel GetEmptyPhotographerModel()
        {
            return new PhotographerModel();
        }

        public BIF.SWE2.Interfaces.ViewModels.IPhotographerViewModel GetEmptyPhotographerViewModel()
        {
            return new PhotographerViewModel(new PhotographerModel());
        }

        public BIF.SWE2.Interfaces.ViewModels.IPictureListViewModel GetEmptyPictureListViewModel()
        {
            return new PictureListViewModel(new List<IPictureModel>());
        }

        public BIF.SWE2.Interfaces.Models.IPictureModel GetEmptyPictureModel()
        {
            return new PictureModel("test.jpg");
        }

        public BIF.SWE2.Interfaces.ViewModels.IPictureViewModel GetEmptyPictureViewModel()
        {
            return new PictureViewModel(new PictureModel("test.jpg"));
        }

        public BIF.SWE2.Interfaces.ViewModels.ISearchViewModel GetEmptySearchViewModel()
        {
            return new SearchViewModel();
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

        public ICameraModel GetEmptyCameraModel()
        {
            return new CameraModel();
        }

        public ICameraListViewModel GetEmptyCameraListViewModel()
        {
            return new CameraListViewModel(new List<ICameraModel>());
        }

        public ICameraViewModel GetEmptyCameraViewModel()
        {
            return new CameraViewModel(new CameraModel());
        }
    }
}
