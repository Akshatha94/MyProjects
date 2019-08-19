using ChamaAssignment.Service.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChamaAssignment.WebApi.Controllers
{
    public class EnrollmentController : ApiController
    {
        IEnrollmentService myEnrollmentService;

        public EnrollmentController(IEnrollmentService theEnrollmentService)
        {
            myEnrollmentService = theEnrollmentService;
        }

        [HttpPost]
        //api/enrollment/SignUp/theCourseId/theStudentId
        public HttpResponseMessage SignUp(string theCourseId, string theStudentId)
        {
            HttpResponseMessage aResponse = Request.CreateResponse(HttpStatusCode.OK, "value1");
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelState);
            }

            try
            {
                if (this.myEnrollmentService.AddEnrollment(theCourseId, theStudentId))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Entry is added successfully");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal error occurred while insering data");
            }
            catch(KeyNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch(DbUpdateException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error occurred");
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        //api/enrollment/SignUpAsync/theCourseId/theStudentId
        public async Task<HttpResponseMessage> SignUpAsync(string theCourseId, string theStudentId)
        {
            HttpResponseMessage aResponse = Request.CreateResponse(HttpStatusCode.OK, "value1");
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ModelState);
            }

            try
            {
                if (await this.myEnrollmentService.AddEnrollmentAsync(theCourseId, theStudentId))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Entry is added successfully");
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal error occurred while insering data");
            }
            catch (KeyNotFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (DbUpdateException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error occurred");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
