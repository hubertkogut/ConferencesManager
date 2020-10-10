using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ConferencesProject.Models;

namespace ConferencesProject
{
    public class ExceptionLogFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request
                .Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        Message = "An error has occured. Please try again later",
                    }
                };
            }

            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.ExceptionHandled = true;


            var _context = new ConferenceContext();
            var error = new ErrorLog()
            {
                Message = filterContext.Exception.Message,
                ControllerName = filterContext.Controller.GetType().Name,
                Timestamp = DateTime.Now
            };
            _context.Errors.Add(error);
            _context.SaveChanges();


            MailMessage email = new MailMessage();
            email.From = new MailAddress("ErrorOccured@o2.pl");
            email.To.Add(new MailAddress(ConfigurationManager
                .AppSettings["ErrorEmail"]));
            email.Subject = "An Error occured";
            email.Body = filterContext.Exception.Message + Environment.NewLine
                                                         + filterContext.Exception.StackTrace;
            SmtpClient client = new SmtpClient("localhost", 25);
            client.Send(email);


        }
    }
}
