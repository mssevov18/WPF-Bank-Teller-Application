﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Teller_App.Data.Models
{
    public partial class Person
    {
        public Person()
        {
            Accounts = new HashSet<Account>();
            BankWorkers = new HashSet<BankWorker>();
        }

        public Person(
            string Egn,
            string FirstName,
            string MiddleName,
            string LastName,
            string Residence,
            DateTime BirthDay
            )
        {
            this.Egn = Egn;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Residence = Residence;
            this.BirthDay = BirthDay;

            Accounts = new HashSet<Account>();
            BankWorkers = new HashSet<BankWorker>();
        }

        public string Egn { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Residence { get; set; }
        public DateTime BirthDay { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<BankWorker> BankWorkers { get; set; }
    }
}
