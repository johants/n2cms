﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace N2.Web.Mvc
{
	public static class ViewEnginesExtensions
	{
		/// <summary>Register the <see cref="ThemeViewEngine"/> which redirects view renderings to a themed counterpart if available.</summary>
		/// <typeparam name="T">The type of view engine to use as base.</typeparam>
		/// <param name="viewEngines">Placeholder.</param>
		/// <returns>The theme view engine that was inserted.</returns>
		public static ThemeViewEngine<T> RegisterThemeViewEngine<T>(this ViewEngineCollection viewEngines) where T : VirtualPathProviderViewEngine, new()
		{
			var tve = new ThemeViewEngine<T>();
			viewEngines.Insert(0, tve);
			return tve;
		}

		/// <summary>Wraps all currently registered view engines enabling the template first developerment paradigm.</summary>
		/// <param name="viewEngines">Placeholder.</param>
		/// <remarks>To enable template first you must also explictly add controllers using the <see cref="N2.Definitions.Runtime.ViewTemplateRegistrator"/>.</remarks>
		public static void DecorateViewTemplateRegistration(this ViewEngineCollection viewEngines)
		{
			for (int i = 0; i < viewEngines.Count; i++)
			{
				viewEngines[i] = new RegisteringViewEngineDecorator(viewEngines[i]);
			}
		}
	}
}