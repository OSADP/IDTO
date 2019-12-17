﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Repository;



namespace IDTO.Entity.Models
{
    public class Mode : EntityBase
    {
        //Primary Key
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
