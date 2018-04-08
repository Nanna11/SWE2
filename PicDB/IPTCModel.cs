﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces;
using BIF.SWE2.Interfaces.Models;

namespace PicDB
{
    public class IPTCModel : IIPTCModel
    {
        string _Keywords;
        string _ByLine;
        string _CopyrightNotice;
        string _Headline;
        string _Caption;

        public IPTCModel()
        {
        }

        public string Keywords {
            get => _Keywords;
            set => _Keywords = value;
        }

        public string ByLine {
            get => _ByLine;
            set => _ByLine = value;
        }

        public string CopyrightNotice {
            get => _CopyrightNotice;
            set => _CopyrightNotice = value;
        }

        public string Headline {
            get => _Headline;
            set => _Headline = value;
        }

        public string Caption {
            get => _Caption;
            set => _Caption = value;
        }
    }
}
