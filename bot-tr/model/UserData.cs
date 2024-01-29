using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bot_tr.model
{


    class UserData
    {
        public string? UserName { get; set; }
        [Key]
        public long UserId { get; set; }
        public string? AccountName { get; set; }
    }
}
