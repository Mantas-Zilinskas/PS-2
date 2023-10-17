using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    public class DarkModeManager
    {
        public static DarkModeSettings GetDarkModeSettings(HttpContext httpContext)
        {
           
            DarkModeSettings darkModeSettings;

            if (httpContext.Session.TryGetValue("DarkModeSettings", out var darkModeBytes))
            {
                darkModeSettings = JsonConvert.DeserializeObject<DarkModeSettings>(Encoding.UTF8.GetString(darkModeBytes));
            }
            else
            {
                darkModeSettings = new DarkModeSettings();
            }

            return darkModeSettings;
        }

        public static void ToggleDarkMode(HttpContext httpContext)
        {
           
            DarkModeSettings darkModeSettings = GetDarkModeSettings(httpContext);

       
            darkModeSettings.IsDarkModeEnabled = !darkModeSettings.IsDarkModeEnabled;

          
            var updatedDarkModeBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(darkModeSettings));
            httpContext.Session.Set("DarkModeSettings", updatedDarkModeBytes);
        }
    }
}
