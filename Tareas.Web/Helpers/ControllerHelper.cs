using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crm.Web.Helpers
{
    public static class ControllerHelper
    {
        /// <summary>
        /// Muestra un mensaje informativo después de realizar alguna acción
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        public static void InfoMessage(this Controller controller, string message)
        {
            controller.TempData["InfoMessage"] = message;
        }

        /// <summary>
        /// Muestra un mensaje informativo cuando ocurre un error
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        public static void ErrorMessage(this Controller controller, string message)
        {
            controller.TempData["ErrorMessage"] = message;
        }

        /// <summary>
        /// Muestra un mensaje informativo cuando ocurre un error. Si el proyecto está
        /// en modo Debug, muestra la exceptión en pantalla, de lo contrario, se lleva
        /// a un log.
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        /// <param name="ex">Excepción generada por el error</param>
        public static void ErrorMessage(this Controller controller, string message, Exception ex)
        {
            controller.TempData["ErrorMessage"] = message;
#if DEBUG
            controller.TempData["ExceptionMessage"] = ex.ToString();
#else
            //Log.Write(message, ex);
#endif
        }        
    }
}
