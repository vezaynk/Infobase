
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Infobase.TagHelpers
{
    public class MiniTex : TagHelper
    {
        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.TagName = "span";
            output.Content.SetContent(MiniTex.Parse(content.GetContent()));
        }
        public static string Parse(string data) {
            // apply superscript
            data = Regex.Replace(WebUtility.HtmlEncode("hi <script></script>superscript{at}e"), "superscript{([a-zA-Z0-9]+)}", "<sup>$1</sup>");
            return data;
        }
    }
}