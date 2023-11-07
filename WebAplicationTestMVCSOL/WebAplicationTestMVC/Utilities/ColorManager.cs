using System.Text.RegularExpressions;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    // Custom exception type
    public class CustomCookieNameException : Exception
    {
        public CustomCookieNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

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
                        List<StudySetColor> usedColors = studySets.Select(s => s.Color).ToList();
                        List<StudySetColor> availableColors = Enum.GetValues(typeof(StudySetColor))
                            .Cast<StudySetColor>()
                            .Except(usedColors)
                            .ToList();

                        Random random = new Random();
                        StudySetColor randomColor = availableColors[random.Next(availableColors.Count)];

                        studySet.Color = randomColor;
                        usedColors.Add(randomColor);

                        httpContext.Response.Cookies.Append(cookieName, studySet.Color.ToString());
                    }
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    // Rethrow the exception or handle it as needed
                    throw new CustomCookieNameException("An error occurred while assigning colors to study sets.", ex);
                }
            }
        }

        private static string SanitizeCookieName(string input)
        {
            // Replace characters that are not allowed in cookie names with underscores
            string sanitizedName = Regex.Replace(input, "[^a-zA-Z0-9_-]", "_");

            return sanitizedName;
        }

        private static void LogException(Exception ex)
        {
            // Log the exception details to a file or server
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

