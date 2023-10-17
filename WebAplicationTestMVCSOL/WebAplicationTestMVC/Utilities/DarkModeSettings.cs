namespace WebAplicationTestMVC.Utilities
{
    public struct DarkModeSettings
    {
        public bool IsDarkModeEnabled { get; set; }

        public void EnableDarkMode()
        {
            IsDarkModeEnabled = true;
        }

        public void DisableDarkMode()
        {
            IsDarkModeEnabled = false;
        }
    }

}
