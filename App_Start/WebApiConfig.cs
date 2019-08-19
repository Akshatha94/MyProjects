using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using ChamaAssignment.Service.Service.Interfaces;
using ChamaAssignment.Service.Service;
using Microsoft.Practices.Unity;
using ChamaAssignment.Data;

namespace ChamaAssignment.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var container = new UnityContainer();

            //Registering service layer instance and injecting DBContext class
            container.RegisterType<IEnrollmentService, EnrollmentService>(new PerThreadLifetimeManager(), new InjectionConstructor(new CourseDBEntities()));
            container.RegisterType<ICourseService, CourseService>(new PerThreadLifetimeManager(), new InjectionConstructor(new CourseDBEntities()));
            config.DependencyResolver = new UnityResolver(container);

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("SignUpAsync", "Api/{controller}/{action}/{theCourseId}/{theStudentId}",
                defaults: new
                {
                    action = "SignupAsync",
                    cid = RouteParameter.Optional,
                    sid = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute("SignUp", "Api/{controller}/{action}/{theCourseId}/{theStudentId}",
                defaults: new { action = "Signup", 
                    cid = RouteParameter.Optional, 
                    sid = RouteParameter.Optional});

            config.Routes.MapHttpRoute(
                name: "GetCourseInfo",
                routeTemplate: "api/{controller}/{action}/{theCourseId}",
                defaults: new { action = "GetCourseInfo",
                theCourseId = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
