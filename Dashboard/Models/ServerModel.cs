using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class ServerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServerId { get; set; }
        public string ServerName { get; set; }

        public int? ServerGroupId { get; set; }
        public ServerGroupModel ServerGroup { get; set; }

        public ICollection<DriverModel> Drivers { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
    }
}
