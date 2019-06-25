
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Infobase.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";    // Replaces <email> with <a> tag
            var content = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(MiniTexTagHelper.Parse(content.GetContent(NullHtmlEncoder.Default)));
        }
    }

    [HtmlTargetElement("MiniTex")]
    public class MiniTexTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";    // Replaces <email> with <a> tag
            var content = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(MiniTexTagHelper.Parse(content.GetContent(NullHtmlEncoder.Default)));
        }
        public static string Parse(string data) {
            // apply superscript
            data = Regex.Replace(data, "\\\\superscript{([a-zA-Z0-9]+)}", "<sup>$1</sup>", RegexOptions.Compiled);
            return data;
        }
    }
}