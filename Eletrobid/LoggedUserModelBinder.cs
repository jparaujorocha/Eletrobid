using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eletrobid
{
    public class LoggedUserModelBinder : IModelBinder
    {
        private const string UserDataSessionKey = "userdata";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindindContext)
        {
            if (bindindContext.Model != null)
                throw new InvalidOperationException("Não é possível alterar as instâncias.");


            LoggedUser uData = (LoggedUser)controllerContext.HttpContext.Session[UserDataSessionKey];

            if (uData == null)
            {
                string returnUrl = controllerContext.HttpContext.Request.Path;
                controllerContext.HttpContext.Response.Redirect("~/Login/Index/?returnUrl=" + returnUrl);
                controllerContext.HttpContext.Session["msg"] = "Sessão finalizada por inatividade!<br />Por favor efetue o login novamente.";
            }
            
            return uData;
        }
    
    }
}