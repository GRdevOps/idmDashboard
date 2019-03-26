using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class DriverModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; }
        public String DriverName { get; set; }
        public int CurrentCache { get; set; }
        public int PreviousCache { get; set; }
        public string Status { get; set; }

        public int? ServerId { get; set; }
        public ServerModel Server { get; set; }

        public ICollection<MessageModel> Messages { get; set; }
    }
}
