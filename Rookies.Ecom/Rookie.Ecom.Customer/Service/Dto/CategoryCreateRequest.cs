﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Dto
{
    public class CategoryCreateRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateCreate { get; set; }
    }
}
