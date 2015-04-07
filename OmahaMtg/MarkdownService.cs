using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg
{
    public class MarkdownService
    {
        public static string GetHtmlFromMarkdown(string markdownText)
        {
            var md = new MarkdownDeep.Markdown();
            md.ExtraMode = true;
            md.SafeMode = false;

            return md.Transform(markdownText);
        }
    }
}
