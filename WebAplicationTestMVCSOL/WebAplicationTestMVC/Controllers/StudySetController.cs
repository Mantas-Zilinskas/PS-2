using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAplicationTestMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudySetController : ControllerBase
    {
        private static List<StudySetModel> studySets = new List<StudySetModel>();

        [HttpPost("CreateStudySet")]
        public IActionResult CreateStudySet([FromBody] StudySetModel studySet)
        {
            try
            {
                studySets.Add(studySet);

                return Ok(new { message = "Study set created successfully" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = "Failed to create study set", error = ex.Message });
            }
        }
    }

    public class StudySetModel
    {
        public string Name { get; set; }
    }
}
