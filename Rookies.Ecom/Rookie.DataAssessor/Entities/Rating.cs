﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAssessor.Entities
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }

    }
}
