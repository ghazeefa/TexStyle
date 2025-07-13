using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.Extensions {
    internal static class ListExtensions {
        internal static List<SelectListItem> ToSelectItemList(dynamic values) {
            List<SelectListItem> tempList = null;
            if (values != null) {
                tempList = new List<SelectListItem>();
                foreach (var v in values) {
                    tempList.Add(new SelectListItem { Text = v.Name, Value = Convert.ToString(v.Id) });
                }
                tempList.TrimExcess();
            }
            return tempList;
        }

        public static List<SelectListItem> ToSelectList<T>(this ICollection<T> values, string NameProp = null) {
            List<SelectListItem> tempList = null;
            if (values != null) {
                tempList = new List<SelectListItem>();
                foreach (var v in values) {
                    // Using Try to avoid NullRefException when name is null in db
                    try {
                        // this is basic 
                        //var name = v.GetType().GetProperty("Name").GetValue(v).ToString();
                        //var desc = v.GetType().GetProperty("Description").GetValue(v).ToString();
                        

                        var id = v.GetType().GetProperty("Id").GetValue(v).ToString();
                        var text = id;

                        if (string.IsNullOrEmpty(NameProp))
                        {
                            var txtProp = v.GetType().GetProperties().ToList()
                            .Where(x => x.Name.Equals("name", StringComparison.OrdinalIgnoreCase) ||
                                        x.Name.Equals("description", StringComparison.OrdinalIgnoreCase))
                            .FirstOrDefault();

                            if (txtProp != null)
                                text = txtProp.GetValue(v).ToString();
                        }
                        else
                        {
                            text = v.GetType().GetProperty(NameProp).GetValue(v).ToString();
                        }

                        //var text = string.IsNullOrEmpty(name) ? (string.IsNullOrEmpty(desc) ? "" : desc) : name;
                        // TODO: verify if this works
                        //var name = v.GetType().GetProperty("Name").GetValue(v).ToString();
                        //var description = v.GetType().GetProperty("Description").GetValue(v).ToString();
                        //var text = !string.IsNullOrEmpty(name) ? name :
                        //            !string.IsNullOrEmpty(description) ? description : "";
                            //v.GetType().GetProperties()[1].GetValue(v).ToString();
                        tempList.Add(new SelectListItem { Text = string.IsNullOrEmpty(text)? id : text, Value = id });;
                    } catch (Exception lex) {
                        throw lex;
                    }
                }
                tempList.TrimExcess();
            }
            return tempList;
        }

    }
}
