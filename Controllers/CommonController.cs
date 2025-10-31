﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TAS.TagHelpers;

namespace TAS.Controllers
{
	public class CommonController : Controller
	{
		ConnectDbHelper dbHelper = new ConnectDbHelper();

		//Set cookie language
		public IActionResult SetLanguageCookie(string culture, string returnUrl)
		{
			var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				cookieValue,
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);
			return LocalRedirect(returnUrl);
		}
	}
}
