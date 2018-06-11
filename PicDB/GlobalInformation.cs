using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    /// <summary>
    /// holding configuration needed in whole application
    /// </summary>
    public class GlobalInformation
    {
        private static GlobalInformation _instance = null;
        string _folder = null;
        
        private GlobalInformation(string folder)
        {
            _folder = folder;
        }

        /// <summary>
        /// set initial folder for pictures to be manage
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
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

        /// <summary>
        /// destroy instance and folder that is set
        /// </summary>
        public static void Uninitialize()
        {
            _instance = null;
        }

        /// <summary>
        /// returns instance of global information
        /// </summary>
        public static GlobalInformation Instance
        {
            get
            {
                if (_instance == null) throw new SingletonNotInitializedException();
                else return _instance;
            }
        }

        /// <summary>
        /// get/set the path for the pictures to be managed
        /// </summary>
        public string Folder
        {
            get
            {
                return _folder;
            }

            set
            {
                if (String.IsNullOrEmpty(value)) throw new PathNotSetException("Path was null or empty");
                _folder = value;
            }
        }
    }
}
