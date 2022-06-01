using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Teller_App.Interfaces
{
    interface IUserControlFormHandler
    {
        public bool AreAllFieldsEmpty();
        public void ClearAllFields();

    }
}
