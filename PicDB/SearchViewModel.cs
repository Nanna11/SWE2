using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PicDB
{
    public class SearchViewModel : ISearchViewModel
    {
        string _SearchText;
        public delegate void SearchActivatedEventHander(object sender, SearchEventArgs e);
        public event SearchActivatedEventHander SearchActivated;

        /// <summary>
        /// command for activating search
        /// </summary>
        public ICommand OnEnterOrReturn
        {
            get
            {
                return new ActionCommand(() =>
                {
                    OnSearchActivted();
                });
            }
        }

        /// <summary>
        /// The search text
        /// </summary>
        public string SearchText {
            get
            {
                if (string.IsNullOrEmpty(_SearchText)) return null;
                else return _SearchText;
            }
            set
            {
                _SearchText = value;
            }
        }

        /// <summary>
        /// True, if a search is active
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (string.IsNullOrEmpty(SearchText)) return false;
                else return true;
            }
        }

        /// <summary>
        /// Number of photos found.
        /// </summary>
        public int ResultCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected void OnSearchActivted()
        {
            if (SearchActivated != null) SearchActivated(this, new SearchEventArgs(_SearchText));
        }
    }
}
