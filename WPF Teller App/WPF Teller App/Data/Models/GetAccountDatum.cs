using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Teller_App.Data.Models
{
    public partial class GetAccountDatum
    {
        public string AccountFullName { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public int? CardAmount { get; set; }
    }
}
