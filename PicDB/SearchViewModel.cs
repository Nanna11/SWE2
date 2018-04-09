using BIF.SWE2.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PicDB
{
    public class SearchViewModel : ISearchViewModel//, INotifyPropertyChanged
    {
        string _SearchText;
        public delegate void SearchActivatedEventHander(object sender, SearchEventArgs e);
        public event SearchActivatedEventHander SearchActivated;
        public ICommand OnEnterOrReturn
        {
            get
            {
                //cache
                return new ActionCommand(() =>
                {
                    OnSearchActivted();
                });
            }
        }

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

        public bool IsActive
        {
            get
            {
                if (string.IsNullOrEmpty(SearchText)) return false;
                else return true;
            }
        }

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
