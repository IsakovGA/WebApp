using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebApplication2.Helpers
{
    public static class Paging
    {
        public static MvcHtmlString PagingNavigator(this HtmlHelper helper, int pageNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(helper.ActionLink("<", "ListOfUsers", new { pageNumber = (pageNumber - 1) }));
            sb.Append(helper.ActionLink(">", "ListOfUsers", new { pageNumber = (pageNumber + 1) }));

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}