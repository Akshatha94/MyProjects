using AppInterface;
using ChamaAssignment.Service.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ChamaAssignment.WebApi.Controllers
{
    public class CourseController : ApiController
    {
        ICourseService myCourseService;

        public CourseController(ICourseService theCourseService)
        {
            myCourseService = theCourseService;
        }

        [HttpGet]
        [ResponseType(typeof(IList<StudentAgeReport>))]
        public IHttpActionResult GetAgeReport()
        {
            var aStAgeReport = this.myCourseService.GetStudentAgeReport();
            if(aStAgeReport == null || aStAgeReport.Count == 0)
            {
                return NotFound();
            }
            return Ok(aStAgeReport);
        }

        [HttpGet]
        [ResponseType(typeof(CourseInfo))]
        public IHttpActionResult GetCourseInfo(string theCourseId)
        {
            var aCourse = this.myCourseService.GetCourseInfo(theCourseId);
            if(aCourse == null)
            {
                return NotFound();
            }
            return Ok(aCourse);
        }
    }
}
