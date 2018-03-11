using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;
using System.ComponentModel;

namespace PicDB
{
    class IPTCViewModel : IIPTCViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IIPTCModel _IPTCModel;
        IEnumerable<string> _CopyrightNotices = new List<string>()
        {
            "All rights reserved",
            "CC-BY",
            "CC-BY-SA",
            "CC-BY-ND",
            "CC-BY-NC",
            "CC-BY-NC-SA",
            "CC-BY-NC-ND"
        };

        public IPTCViewModel(IIPTCModel im)
        {
            _IPTCModel = im;
        }

        public string Keywords {
            get => _IPTCModel.Keywords;
            set
            {
                _IPTCModel.Keywords = value;
                OnPropertyChanged("Keywords");
            }
        }

        public string ByLine {
            get => _IPTCModel.ByLine;
            set { _IPTCModel.ByLine = value; OnPropertyChanged("ByLine"); }
            }

        public string CopyrightNotice {
            get => _IPTCModel.CopyrightNotice;
            set { _IPTCModel.CopyrightNotice = value; OnPropertyChanged("CopyrightNotice"); }
            }

        public IEnumerable<string> CopyrightNotices => _CopyrightNotices;

        public string Headline {
            get => _IPTCModel.Headline;
            set { _IPTCModel.Headline = value; OnPropertyChanged("Headline"); }
            }

        public string Caption {
            get => _IPTCModel.Caption;
            set { _IPTCModel.Caption = value; OnPropertyChanged("Caption"); }
            }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
