using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_Teller_App.Data.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public string Requester { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsProcessed { get; set; }
        public string TableAffected { get; set; }
        public bool? WillDelete { get; set; }
        public string Arguments { get; set; }

        public Request(string Requester,
                       DateTime Timestamp,
                       bool IsProcessed,
                       string TableAffected,
                       bool? WillDelete,
                       string Arguments)
        {
            this.Requester = Requester;
            this.Timestamp = Timestamp;
            this.IsProcessed = IsProcessed;
            this.TableAffected = TableAffected;
            this.WillDelete = WillDelete;
            this.Arguments = Arguments;
        }
    }
}
