using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StayFitz.Resources.Localization;

namespace StayFitz.Helper
{
    public class LocalizationResourceManager
    {
        private static LocalizationResourceManager _instance;
        public static LocalizationResourceManager Instance =>
            _instance ??= new LocalizationResourceManager();

        public string this[string key]
        {
            get => AppResources.ResourceManager.GetString(key, AppResources.Culture);
        }

        public void SetCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            AppResources.Culture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
