namespace Whosales.Web.Extentions
{
	public static class DateViewExtention
	{
		public static string ToDatePickerView(this DateTime date)
		{
			return $"{date.ToString("yyyy-MM-ddThh:mm")}";
		}
	}
}
