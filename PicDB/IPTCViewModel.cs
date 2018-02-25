using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.ViewModels;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    class IPTCViewModel : IIPTCViewModel
    {
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
            set => _IPTCModel.Keywords = value;
        }

        public string ByLine {
            get => _IPTCModel.ByLine;
            set => _IPTCModel.ByLine = value;
        }

        public string CopyrightNotice {
            get => _IPTCModel.CopyrightNotice;
            set => _IPTCModel.CopyrightNotice = value;
        }

        public IEnumerable<string> CopyrightNotices => _CopyrightNotices;

        public string Headline {
            get => _IPTCModel.Headline;
            set => _IPTCModel.Headline = value;
        }

        public string Caption {
            get => _IPTCModel.Caption;
            set => _IPTCModel.Caption = value;
        }
    }
}
