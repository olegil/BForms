﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Bibliography;

namespace BForms.Grid
{
    public class BsGridRepositorySettings<TSearch> : BsGridBaseRepositorySettings
    {
        public TSearch Search { get; set; }

        public string QuickSearch { get; set; }

        public List<BsColumnOrder> OrderableColumns { get; set; } // order grid by column

        public BsGridBaseRepositorySettings BaseSettings
        {
            get
            {
                return new BsGridBaseRepositorySettings
                {
                    GetDetails = this.GetDetails,
                    OrderColumns = this.OrderColumns,
                    Page = this.Page,
                    PageSize = this.PageSize
                };
            }
        }
    }

    public class BsGridBaseRepositorySettings
    {
        public Dictionary<string, int> OrderColumns { get; set; } // swap columns order

        public int Page { get; set; }

        public int PageSize { get; set; }

        public bool GetDetails { get; set; }

        public BsGridBaseRepositorySettings()
        {
            this.Page = 1;
            this.PageSize = 5;
        }
    }
}