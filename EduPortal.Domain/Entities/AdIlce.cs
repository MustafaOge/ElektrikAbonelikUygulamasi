﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Domain.Entities
{
    public class Ad_Ilce
    {
        [Key]
        public int ilceKimlikNo { get; set; }   
        public string adi {  get; set; }

    }
}
