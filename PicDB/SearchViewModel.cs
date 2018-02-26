using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicDB
{
    class SearchViewModel : ISearchViewModel
    {
        string _SearchText;


        public string SearchText {
            get => _SearchText;
            set => _SearchText = value;
        }

        public bool IsActive
        {
            get
            {
                if (string.IsNullOrEmpty(SearchText)) return false;
                else return true;
            }
        }

        public int ResultCount => throw new NotImplementedException();
    }
}
