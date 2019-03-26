using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class ServerGroupModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServerGroupId { get; set; }
        public string ServerGroupName { get; set; }

        public ICollection<ServerModel> Servers { get; set; }
    }
}
