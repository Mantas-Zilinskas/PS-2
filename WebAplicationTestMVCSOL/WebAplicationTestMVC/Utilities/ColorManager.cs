using Microsoft.AspNetCore.Http; // Add this namespace
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Utilities
{
    public class ColorManager
    {
        public static void AssignUniqueColor(List<StudySet> studySets, HttpContext httpContext)
        {
            List<StudySetColor> usedColors = new List<StudySetColor>();

            foreach (var studySet in studySets)
            {
                
                string cookieName = $"Color_{studySet.studySetName.Replace(" ", "_").Replace(".", "_")}";

                if (httpContext.Request.Cookies.TryGetValue(cookieName, out var color))
                {
                    studySet.Color = (StudySetColor)Enum.Parse(typeof(StudySetColor), color);
                }
                else
                {
                   
                    StudySetColor randomColor;
                    Random random = new Random();

                    
                    do
                    {
                        randomColor = (StudySetColor)random.Next(Enum.GetValues(typeof(StudySetColor)).Length);
                    } while (usedColors.Contains(randomColor));

                    studySet.Color = randomColor;
                    usedColors.Add(randomColor);

                
                    httpContext.Response.Cookies.Append(cookieName, studySet.Color.ToString());
                }
            }
        }
    }
}
