﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Specifications.SpecificationParams
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;
        private int _pageSize = 5;
        private string _search;

        public int PageIndex { get; set; } = 1;        
        public int PageSize 
        { 
            get => _pageSize; 
            set => _pageSize = (value > MaxPageSize)?MaxPageSize : value; 
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search 
        { 
            get => _search; 
            set => _search = value.ToLower(); 
        }
    }
}
