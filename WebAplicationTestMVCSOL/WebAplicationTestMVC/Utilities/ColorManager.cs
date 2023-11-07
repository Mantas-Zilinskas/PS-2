using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Models;
using WebAplicationTestMVC.Services;
namespace WebAplicationTestMVC.Utilities
{
    public class ColorManager
    {
        public static void AssignUniqueColor(List<StudySet> studySets, HttpContext httpContext)
        {
            foreach (var studySet in studySets)
            {
                string cookieName = $"Color_{studySet.StudySetName.Replace(" ", "_").Replace(".", "_")}";

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
        }
    }
}
