using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PicDB
{
    /// <summary>
    /// command to be binded to control action
    /// </summary>
    public class ActionCommand : ICommand
    {
        private readonly Action _action;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="action">action that will be executed</param>
        public ActionCommand(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// execute action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
