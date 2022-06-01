using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Teller_App.Pages
{
    /// <summary>
    /// Allows for Pages to handle user input and saving before closing the page
    /// </summary>
    public interface IPageClosingHandler
    {
        public bool Close();
        public void ClearAllFields();
    }
}
