using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class MessageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        public string MessageType { get; set; }
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }

        public int ServerId { get; set; }
        public ServerModel Server { get; set; }

        public int? DriverId { get; set; }
        public DriverModel Driver { get; set; }
    }
}
