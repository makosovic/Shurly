using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Shurly.Core.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAccountId : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (filterContext.ActionArguments.ContainsKey("accountId"))
            {
                if (filterContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    filterContext.ActionArguments["accountId"] = filterContext.RequestContext.Principal.Identity.Name;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}