﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXSM3942_Demo.Data.Models
{
    public class ClassRoom
    {
        public int ID { get; set; }
        public int RoomNumber { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
