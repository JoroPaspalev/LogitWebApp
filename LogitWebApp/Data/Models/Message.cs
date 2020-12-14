using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogitWebApp.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsRead { get; set; }        

       
    }
}
