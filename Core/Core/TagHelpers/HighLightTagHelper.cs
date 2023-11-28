using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.TagHelpers;

[HtmlTargetElement("*", Attributes = "[highlight=true]")]
public class HighLightTagHelper : TagHelper
{
    public string WrapperString { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.PreContent.SetHtmlContent("<b><i>");
        output.PostContent.SetHtmlContent("</i></b>");
    }
}