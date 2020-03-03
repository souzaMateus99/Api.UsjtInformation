using System;
using System.Web;
using System.Linq;
using ScrapySharp.Html;
using USJT.Service.Enums;
using ScrapySharp.Network;
using USJT.Models.Extensions;
using System.Threading.Tasks;
using USJT.Service.Constants;
using System.Collections.Generic;
using System.Collections.Immutable;
using HtmlAgilityPack;

namespace USJT.Service.Pages
{
    public abstract class PageBase
    {
        internal readonly ScrapingBrowser browser;
        
        public PageBase(ScrapingBrowser browser)
        {
            this.browser = browser;
        }

        public KeyValuePair<string, string> GetSideBarMenuItem(string item, WebPage page)
        {
            return ExpandSideBarMenu(page).FirstOrDefault(m => string.Compare(m.Key, item, true) == 0);
        }

        public IDictionary<string, string> ExpandSideBarMenu(WebPage page)
        {
            var sideBarMenu = page.Find(HtmlTags.Div.GetDescription(), By.Class("menu-list")).FirstOrDefault();

            if(sideBarMenu is null)
            {               
                return ImmutableDictionary<string, string>.Empty;
            }

            var nodesMenu = sideBarMenu.OwnerDocument.DocumentNode.SelectNodes("//ul //a");
            var dic = new Dictionary<string, string>();
            
            foreach(var node in nodesMenu)
            {
                var title = node.InnerText;
                var link = node.Attributes.AttributesWithName("href").FirstOrDefault().Value;

                dic.TryAdd(title, link);
            }

            return dic;
        }

        public string GetParameterString(Dictionary<string, string> parameters)
        {
            var parametersEncoded = parameters.Select(p => 
            {
                var key = HttpUtility.UrlEncode(p.Key);
                var value = HttpUtility.UrlEncode(p.Value);

                return $"{key}={value}";
            });

            return string.Join("&", parametersEncoded);
        }
    }
}