using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed
{
    public class Common
    {
        [NonAction]
        public SelectList ToSelectList (DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }
    }
    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable <T>(List<T> items)
        {
            DataTable datatable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(PropertyInfo prop in Props)
            {
                datatable.Columns.Add(prop.Name);

            }
            foreach(T item in items)
            {
                var values = new object[Props.Length];
                for (int i=0; i<Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                datatable.Rows.Add(values);
            }
            return datatable;
        }
        public class ProductType
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}