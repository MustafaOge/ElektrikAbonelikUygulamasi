﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Domain.Entities
{
    public class Ad_Sokak
    {
        [Key]
        public long sokakKimlikNo { get; set; }
        public string adi { get; set; }
        public int mahalleKimlikNo { get; set; }

    }
}
