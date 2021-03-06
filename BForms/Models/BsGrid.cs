﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BForms.Grid;
using BForms.Utilities;

namespace BForms.Models
{
    public abstract class BsItemsModel<T>
    {
        public IEnumerable<T> Items { get; set; }
    }

    public class BsGridModel<T> : BsItemsModel<T>
    {
        public BsPagerModel Pager { get; set; }

        public BsGridBaseRepositorySettings BaseSettings { get; set; }

        public BsGridModel()
        {
            this.BaseSettings = new BsGridBaseRepositorySettings();
        }

        /// <summary>
        /// Helper for wrapping grid
        /// </summary>
        /// <typeparam name="TModel">Wrapper model type</typeparam>
        /// <param name="expression">Grid selector targeted for wrapping</param>
        /// <param name="grid">Grid object</param>
        /// <returns>Wrapper model</returns>
        public TModel Wrap<TModel>(Expression<Func<TModel, BsGridModel<T>>> expression) where TModel: new()
        {
            var model = new TModel();
            var gridProp = expression.GetPropertyInfo();
            gridProp.SetValue(model, this);
            return model;
        }

        public bool IsEmpty()
        {
            return this.Items == null || !this.Items.Any();
        }
    }

    public class BsGridRowData<T>
    {
        public T Id { get; set; }
        public bool GetDetails { get; set; }
    }

    public abstract class BsGridRowModel<TDetails> : BsItemModel
    {
        private TDetails details;

        public TDetails Details
        {
            get
            {
                return this.details;
            }
            set
            {
                this.details = value;
                this.HasDetails = true;
            }
        }

        internal bool HasDetails { get; set; }
    }
}
