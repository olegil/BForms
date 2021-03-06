﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using BForms.Utilities;

namespace BForms.Html
{
    /// <summary>
    /// Represents support for the bootstrap HTML label element
    /// </summary>
    public static class LabelExtensions
    {
        /// <summary>
        /// Returns a label element with required css class
        /// </summary>
        public static MvcHtmlString BsLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return BsLabelFor(htmlHelper, expression, (object) null);
        }

        /// <summary>
        /// Returns a label element with required css class
        /// </summary>
        public static MvcHtmlString BsLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return BsLabelFor(htmlHelper, expression, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Returns a label element with required css class
        /// </summary>
        public static MvcHtmlString BsLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            //merge custom css classes with bootstrap
            htmlAttributes.MergeAttribute("class", "control-label");

            var propertyName = ExpressionHelper.GetExpressionText(expression);
            RequiredAttribute attr;
            if (ReflectionHelpers.TryGetAttribute(propertyName, typeof(TModel), out attr))
            {
                htmlAttributes.MergeAttribute("class", "required");
            }
            

            var name = htmlHelper.AttributeEncode(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName));
            if (typeof(TProperty).FullName.Contains("BsSelectList"))
            {
                propertyName += ".SelectedValues";
            }
            if (typeof(TProperty).FullName.Contains("BsRange") || typeof(TProperty).FullName.Contains("BsDateTime"))
            {
                propertyName += ".TextValue";
            }
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? name.Split('.').Last();

            return htmlHelper.Label(propertyName, labelText, htmlAttributes);
        }
    }
}