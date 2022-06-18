using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizPortal.Models
{
    public static class SelectListFactory
    {
        private static List<SelectListItem> GetListItems(bool firstEmptyItemIncluded = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            if (firstEmptyItemIncluded)
            {
                items.Add(new SelectListItem() { Text = "", Value = "" });
            }

            return items;
        }

        public static SelectList GetEmptyList(bool firstEmptyItemIncluded = false)
        {
            return new SelectList(GetListItems(firstEmptyItemIncluded), "Value", "Text");
        }

        public static SelectList GetOperatingCostType()
        {
            var items = GetListItems(true);
            items.Add(new SelectListItem() { Value = "Fixed", Text = Resources.Application.LABEL_OPERATION_COST_TYPE_FIXED });
            items.Add(new SelectListItem() { Value = "StartAt", Text = Resources.Application.LABEL_OPERATION_COST_TYPE_START_AT });
            items.Add(new SelectListItem() { Value = "Range", Text = Resources.Application.LABEL_OPERATION_COST_TYPE_RANGE });

            return new SelectList(items, "Value", "Text");
        }

        public static SelectList GetOperatingDayType()
        {
            var items = GetListItems(true);
            items.Add(new SelectListItem() { Value = "Fixed", Text = Resources.Application.LABEL_OPERATION_DAY_TYPE_FIXED });
            items.Add(new SelectListItem() { Value = "StartAt", Text = Resources.Application.LABEL_OPERATION_DAY_TYPE_START_AT });
            items.Add(new SelectListItem() { Value = "Range", Text = Resources.Application.LABEL_OPERATION_DAY_TYPE_RANGE });

            return new SelectList(items, "Value", "Text");
        }
    }
}