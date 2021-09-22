using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace GSmartHR.Web.Extensions
{
    public static class HtmlExtension
    {
        #region Date for

        public static IHtmlContent TextBoxDateFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TextBoxDateFor(expression, dict);
        }
        public static IHtmlContent TextBoxDateFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TextBoxDateFor(expression, htmlAttributes);
        }


        public static IHtmlContent TextBoxDateFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData,html.MetadataProvider);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = modelExplorer.Metadata.DisplayName ?? modelExplorer.Metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var value = modelExplorer.Model;

            if (htmlAttributes.ContainsKey("class"))
            {
                var @class = htmlAttributes["class"];

                if (!@class.ToString().Contains("datepicker"))
                {
                    htmlAttributes["class"] = htmlAttributes["class"] + " " + "datepicker";
                }
            }
            else
            {
                htmlAttributes.Add("class", "datepicker");
            }

            htmlAttributes.Add("id", htmlFieldName);
            htmlAttributes.Add("name", htmlFieldName);

            if (value != null)
            {
                DateTime date = Convert.ToDateTime(value);

                if (date > DateTime.MinValue && date > default(DateTime))
                {
                    htmlAttributes.Add("Value", date.ToShortDateString());
                }
                else
                {
                    htmlAttributes.Add("Value", "");
                }
            }


            TagBuilder tagBuilder = new TagBuilder("input");

            foreach (var item in htmlAttributes)
            {
                tagBuilder.Attributes.Add(new KeyValuePair<string, string>(item.Key,item.Value.ToString()));

            }

            return tagBuilder.RenderSelfClosingTag();


 
        }

        #endregion
        #region DateTime for
        public static IHtmlContent TextBoxDateTimeFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TextBoxDateTimeFor(expression, dict);
        }
        public static IHtmlContent TextBoxDateTimeFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TextBoxDateTimeFor(expression, htmlAttributes);
        }

        public static IHtmlContent TextBoxDateTimeFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = modelExplorer.Metadata.DisplayName ?? modelExplorer.Metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var value = modelExplorer.Model;

            if (htmlAttributes.ContainsKey("class"))
            {
                var @class = htmlAttributes["class"];

                if (!@class.ToString().Contains("datetimepicker"))
                {
                    htmlAttributes["class"] = htmlAttributes["class"] + " " + "datetimepicker";
                }
            }
            else
            {
                htmlAttributes.Add("class", "datetimepicker");
            }

            if (value != null)
            {
                DateTime date = Convert.ToDateTime(value);

                if (date > DateTime.MinValue && date > default(DateTime))
                {
                    htmlAttributes.Add("Value", date.ToString("dd/MM/yyyy hh:mm tt"));
                }
                else
                {
                    htmlAttributes.Add("Value", "");
                }



                return html.TextBoxFor(expression, htmlAttributes);
            }
            else
            {

                return html.TextBoxFor(expression, htmlAttributes);
            }

        }
        #endregion
        #region Time For

        public static IHtmlContent TextBoxTimeFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TextBoxTimeFor(expression, dict);
        }
        public static IHtmlContent TextBoxTimeFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TextBoxTimeFor(expression, htmlAttributes);
        }


        public static IHtmlContent TextBoxTimeFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = modelExplorer.Metadata.DisplayName ?? modelExplorer.Metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var value = modelExplorer.Model;

            if (htmlAttributes.ContainsKey("class"))
            {
                var @class = htmlAttributes["class"];

                if (!@class.ToString().Contains("timepicker"))
                {
                    htmlAttributes["class"] = htmlAttributes["class"] + " " + "timepicker";
                }
            }
            else
            {
                htmlAttributes.Add("class", "timepicker");
            }


            return html.TextBoxFor(expression, htmlAttributes);


        }

        #endregion
    }
}
