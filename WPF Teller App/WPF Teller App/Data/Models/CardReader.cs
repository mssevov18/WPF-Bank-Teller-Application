using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Teller_App.Data.Models
{
    public partial class CardReader
    {
        public int ReaderId { get; set; }
        public int BankId { get; set; }
        public string AccountRecieverIban { get; set; }

        public virtual Account AccountRecieverIbanNavigation { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
