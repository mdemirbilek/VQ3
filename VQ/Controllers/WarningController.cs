using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VQ;

namespace VET.Controllers
{
    public class WarningController : Controller
    {
        private VQEntities db = new VQEntities();

        // GET: Warning
        public ActionResult Index(string id)
        {
            int msgId = 0;
            ViewBag.ErrMessage = "Warning!! Hmm, some bad things happened! - ";

            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.ErrMessage += id;

                msgId = int.Parse(id.Trim());
                ErrorMessage em = (ErrorMessage)db.ErrorMessages.FirstOrDefault(x => x.MsgId == msgId);
                if (em != null)
                {
                    ViewBag.ErrMsgEN = em.MessageEN;
                    ViewBag.ErrMsgPL = em.MessagePL;
                    ViewBag.BackController = em.BackController;
                    ViewBag.BackAction = em.BackAction;
                }

            }
            return View();
        }
    }
}