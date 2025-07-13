using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TexStyle.ViewModels;

namespace TexStyle.Infrastructure
{
    [HtmlTargetElement("select", Attributes = "select2")]
    public class Select2TagHelper : TagHelper
    {
        [HtmlAttributeName("select2")]
        public List<SelectListItem> SelectList { get; set; }

        [HtmlAttributeName("select2-ph")]
        public string Placeholder { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(Placeholder))
            {
                var Id = output.Attributes.SingleOrDefault(x => x.Name.Equals("id", StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrEmpty(Id.Value.ToString()))
                    Placeholder = "Select a Value";
                else
                {
                    var name = Regex.Replace(Id.Value.ToString().Replace("Id", ""), "([a-z])([A-Z])", "$1 $2");
                    Placeholder = $"Select {name}";
                }
            }

            var body = $"<option value='-1' selected disabled>{Placeholder}</option>";

            if (SelectList != null)
            {
                foreach (var item in SelectList)
                    body += $"<option value='{item.Value}' {(item.Selected ? "selected" : "")}>{item.Text}</option>";
            }
            else
                output.Attributes.SetAttribute("disabled", "disabled");


            output.Content.AppendHtml(body);

            base.Process(context, output);
        }
    }
}
