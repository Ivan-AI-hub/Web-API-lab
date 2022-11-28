using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Whosales.Web.HtmlHelpers
{
	public static class DropDownHelper
	{
		public static HtmlString EnumerableDropDown<T>(this IHtmlHelper html, IEnumerable<T> items, Func<T, int> idField,
			Func<T, string> ViewField, bool isViewZeroElement = false, int selectedId = 0, string selectName = "")
		{
			var builder = new StringBuilder();
			builder.Append($"<select name='{selectName}' >");
			if (isViewZeroElement)
				builder.Append($"<option value='0'> Any </option>");
			foreach (var item in items)
			{
				if (idField(item) != selectedId)
				{
					builder.Append($"<option value='{idField(item)}'> {ViewField(item)} </option>");
				}
				else
				{
					builder.Append($"<option value='{idField(item)}' selected> {ViewField(item)} </option>");
				}

			}

			builder.Append("</select>");
			return new HtmlString(builder.ToString());
		}
	}
}
