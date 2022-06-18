using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;

namespace BizPortal.Utils.Extensions
{
    public static class HtmlExtension
    {
        public static MvcHtmlString CustomTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, TexEditor texteditor = null)
        {
            var member = expression.Body as MemberExpression;
            var attributes = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);
            var tooltipAttrs = (TooltipAttribute[])member.Member.GetCustomAttributes(typeof(TooltipAttribute), false);

            if (tooltipAttrs.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(tooltipAttrs[0].Message))
                {
                    // create tooltip form message attr
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", tooltipAttrs[0].Message ?? "");
                }
                else if (tooltipAttrs[0].MessageResourceType != null)
                {
                    // create tooltip from resource
                    ResourceManager rm = new ResourceManager(tooltipAttrs[0].MessageResourceType);
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", rm.GetString(tooltipAttrs[0].MessageResourceName) ?? "");
                }
                else
                {
                    // tootip not create
                }
            }

            if (texteditor != null)
            {
                attributes.Add("data-editor", "true");
                attributes.Add("data-editor-height", texteditor.Height.HasValue ? texteditor.Height : 300);
            }

            return htmlHelper.TextAreaFor(expression, attributes);
        }

        public static MvcHtmlString CustomDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectListItem, object htmlAttributes = null)
        {
            var member = expression.Body as MemberExpression;
            var attributes = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);
            var tooltipAttrs = (TooltipAttribute[])member.Member.GetCustomAttributes(typeof(TooltipAttribute), false);

            if (tooltipAttrs.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(tooltipAttrs[0].Message))
                {
                    // create tooltip form message attr
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", tooltipAttrs[0].Message ?? "");
                }
                else if (tooltipAttrs[0].MessageResourceType != null)
                {
                    // create tooltip from resource
                    ResourceManager rm = new ResourceManager(tooltipAttrs[0].MessageResourceType);
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", rm.GetString(tooltipAttrs[0].MessageResourceName) ?? "");
                }
                else
                {
                    // tootip not create
                }
            }

            return htmlHelper.DropDownListFor(expression, selectListItem, attributes);
        }

        public static MvcHtmlString CustomPassWordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var member = expression.Body as MemberExpression;
            var attributes = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);
            var tooltipAttrs = (TooltipAttribute[])member.Member.GetCustomAttributes(typeof(TooltipAttribute), false);

            if (tooltipAttrs.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(tooltipAttrs[0].Message))
                {
                    // create tooltip form message attr
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", tooltipAttrs[0].Message ?? "");
                }
                else if (tooltipAttrs[0].MessageResourceType != null)
                {
                    // create tooltip from resource
                    ResourceManager rm = new ResourceManager(tooltipAttrs[0].MessageResourceType);
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", rm.GetString(tooltipAttrs[0].MessageResourceName) ?? "");
                }
                else
                {
                    // tootip not create
                }
            }

            return htmlHelper.PasswordFor(expression, attributes);
        }

        public static MvcHtmlString CustomTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var member = expression.Body as MemberExpression;
            var attributes = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);
            var tooltipAttrs = (TooltipAttribute[])member.Member.GetCustomAttributes(typeof(TooltipAttribute), false);

            if (tooltipAttrs.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(tooltipAttrs[0].Message))
                {
                    // create tooltip form message attr
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", tooltipAttrs[0].Message ?? "");
                }
                else if (tooltipAttrs[0].MessageResourceType != null)
                {
                    // create tooltip from resource
                    ResourceManager rm = new ResourceManager(tooltipAttrs[0].MessageResourceType);
                    attributes.Add("data-toggle", "tooltip");
                    attributes.Add("data-placement", tooltipAttrs[0].Position ?? "right");
                    attributes.Add("title", rm.GetString(tooltipAttrs[0].MessageResourceName) ?? "");
                }
                else
                {
                    // tootip not create
                }
            }

            return htmlHelper.TextBoxFor(expression, attributes);
        }

        public static MvcHtmlString CustomTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Tooltip tooltip, object htmlAttributes = null)
        {
            var member = expression.Body as MemberExpression;
            var attributes = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);

            if (!string.IsNullOrWhiteSpace(tooltip.message))
            {
                attributes.Add("data-toggle", "tooltip");
                attributes.Add("data-placement", tooltip.position ?? "right");
                attributes.Add("title", tooltip.message ?? "");
            }

            return htmlHelper.TextBoxFor(expression, attributes);
        }

        public static MvcHtmlString CustomValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, FeedbackIcon icon = null)
        {
            string custom;
            if (icon != null)
            {
                if (!string.IsNullOrWhiteSpace(icon.ErrorIcon) || !string.IsNullOrWhiteSpace(icon.SuccessIcon))
                {
                    custom = htmlHelper.ValidationMessageFor(expression).ToString() + string.Format("<span class='form-control-error {0}'></span> <span class='form-control-success {1}'></span>", icon.ErrorIcon, icon.SuccessIcon);
                }
                else
                {
                    custom = htmlHelper.ValidationMessageFor(expression).ToString() + "<span class='form-control-error form-control-feedback glyphicon glyphicon-remove'></span> <span class='form-control-success form-control-feedback glyphicon glyphicon-ok'></span>";
                }
            }
            else
            {
                custom = htmlHelper.ValidationMessageFor(expression).ToString();
            }
            return MvcHtmlString.Create(custom);
        }

        //render patail script work aroud from http://stackoverflow.com/questions/5433531/using-sections-in-editor-display-templates/5433722#5433722
        public static MvcHtmlString PartialScripts(this HtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return MvcHtmlString.Empty;
        }

        public static IHtmlString PartialRenderScripts(this HtmlHelper htmlHelper)
        {
            List<string> htmlResults = new List<string>();
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_script_"))
                {
                    var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        string htmlResult = template(null).ToHtmlString();
                        if (!htmlResults.Contains(htmlResult))
                        {
                            htmlResults.Add(htmlResult);
                            htmlHelper.ViewContext.Writer.Write(template(null));
                        }
                    }
                }
            }
            return MvcHtmlString.Empty;
        }

        public static List<SelectListItem> AddCriteria(this List<SelectListItem> selectList, DropdownlistType type)
        {
            switch (type)
            {
                case DropdownlistType.ALL:
                    selectList.Insert(0, new SelectListItem() { Text = Resources.Global.DDL_SELECT_ALL, Value = "" });
                    break;
                case DropdownlistType.SELECT:
                    selectList.Insert(0, new SelectListItem() { Text = Resources.Global.DDL_SELECT, Value = "" });
                    break;
                default:
                    break;
            }
            return selectList;
        }
    }

    #region[HtmlExtension Model]
    public class Tooltip
    {
        public string position { get; set; } //data_placement
        public string message { get; set; } //title
    }
    public class FeedbackIcon
    {
        public string ErrorIcon { get; set; }
        public string SuccessIcon { get; set; }
    }
    public class TexEditor
    {
        public int? Height { get; set; }
    }
    #endregion
}