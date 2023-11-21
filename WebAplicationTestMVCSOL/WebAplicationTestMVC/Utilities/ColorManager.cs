using System.Text.RegularExpressions;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    public class ColorManager
    {
        public static void AssignUniqueColor(List<StudySet> studySets, HttpContext httpContext)
        {
            foreach (var studySet in studySets)
            {
                try
                {
                    string sanitizedStudySetName = SanitizeCookieName(studySet.StudySetName);
                    string cookieName = $"Color_{sanitizedStudySetName}";

                    if (httpContext.Request.Cookies.TryGetValue(cookieName, out var color))
                    {
                        studySet.Color = (StudySetColor)Enum.Parse(typeof(StudySetColor), color);
                    }
                    else
                    {
                        List<StudySetColor> usedColors = studySets.Select(s => s.Color).Distinct().ToList();
                        List<StudySetColor> availableColors = Enum.GetValues(typeof(StudySetColor))
                            .Cast<StudySetColor>()
                            .Except(usedColors)
                            .ToList();

                        if (availableColors.Count == 0)
                        {
                            throw new AllColorsUsedException("All colors are used.", studySet);
                        }

                        Random random = new Random();
                        StudySetColor randomColor = availableColors[random.Next(availableColors.Count)];
                        studySet.Color = randomColor;
                        httpContext.Response.Cookies.Append(cookieName, studySet.Color.ToString());
                    }
                }
                catch (AllColorsUsedException ex)
                {
                    LogException(ex);
                }
            }
        }

        private static string SanitizeCookieName(string input)
        {
            string sanitizedName = Regex.Replace(input, "[^a-zA-Z0-9_-]", "_");

            return sanitizedName;
        }

        private static void LogException(Exception ex)
        {
            string logFilePath = "error.log";

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"Timestamp: {DateTime.UtcNow}");
                writer.WriteLine($"Exception Message: {ex.Message}");
                writer.WriteLine($"StackTrace: {ex.StackTrace}");
                writer.WriteLine(new string('-', 40));
            }
        }
    }
}

