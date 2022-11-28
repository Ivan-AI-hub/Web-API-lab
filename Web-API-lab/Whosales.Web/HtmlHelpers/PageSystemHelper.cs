using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Whosales.Web.HtmlHelpers
{
	public static class PageSystemHelper
	{
		private static int _displayCount = 3;
		public static HtmlString CreatePageNav(this IHtmlHelper html, string urlName, int pageCount, int selectedPage)
		{
			var builder = new StringBuilder();
			builder.Append("<nav class='container navbar navbar-expand-lg navbar-light bg-light'>" +
							"<div class='collapse navbar-collapse' id='navbarSupportedContent'>" +
							"<ul class='navbar-nav mr-auto'>");

			for (var i = 1; i <= _displayCount; i++)
			{
				builder.Append(CreateList(urlName, i, pageCount, selectedPage).Value);
			}

			if ((int)selectedPage > _displayCount * 2)
			{
				builder.Append("<li class='nav-item'>...</li> ");
			}
			for (var i = selectedPage - _displayCount; i <= selectedPage + _displayCount; i++)
			{
				if (i > _displayCount && i < pageCount - _displayCount)
				{
					builder.Append(CreateList(urlName, i, pageCount, selectedPage).Value);
				}
			}

			if (selectedPage < pageCount - _displayCount * 2)
			{
				builder.Append("<li class='nav-item'>...</li> ");
			}
			for (var i = pageCount - _displayCount; i <= pageCount; i++)
			{
				if (i > _displayCount)
				{
					builder.Append(CreateList(urlName, i, pageCount, selectedPage).Value);
				}
			}
			builder.Append("</ul> </div>  </nav>");

			return new HtmlString(builder.ToString());
		}
		private static HtmlString CreateList(string urlName, int currentPage, int pageCount, int selectedPage)
		{
			string result = "<li class='nav-item'>";
			if (currentPage > 0 && currentPage <= pageCount)
			{
				if (currentPage != selectedPage)
					result += @$"<a class='nav-link' href='/{urlName}/?CurrentPage={currentPage}'>{currentPage}</a>";

				else if (currentPage == selectedPage)
					result += @$"<a class='nav-link' href='/{urlName}/?CurrentPage={currentPage}'><b>{currentPage}</b></a>";

			}
			result += "</li>";
			return new HtmlString(result);
		}

	}
}