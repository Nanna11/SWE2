using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    class GlobalInformation
    {
        private static GlobalInformation _instance = null;
        string _folder = null;
        
        private GlobalInformation(string folder)
        {
            _folder = folder;
        }

        public static GlobalInformation InitializeInstance(string folder)
        {
            if (_instance == null)
            {
                if (string.IsNullOrEmpty(folder)) throw new ArgumentNullException("folder");
                _instance = new GlobalInformation(folder);
                return _instance;
            }
            else throw new SingletonInitializedTwiceException();
        }

        public static GlobalInformation Instance
        {
            get
            {
                if (_instance == null) throw new SingletonNotInitializedException();
                else return _instance;
            }
        }

        public string Folder
        {
            get
            {
                return _folder;
            }
        }
    }
}
