using StoreDomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Link(description = "Каталог", url = "~/Store/Index")]
    public class StoreController : Controller
    {
        [Route("~/Details/{goodTypeId}/{goodId:int}")]
        public ActionResult Details(long goodId)
        {
            try
            {
                var store = new StoreAction();
                var good = store.GetGoodInfo(goodId);
                var model = new GoodDetailsViewModel(good);
                model.feedbacks = store.GetFeedBack(goodId);
                model.canSendFeedback = Session.IsAuth() && !model.feedbacks.Any(s => s.userId == Session.GetUserId());
                if (Session.IsAuth())
                {
                    var customer = new CustomerAction(Session.GetUserId());
                    customer.SetThatWatching(goodId);
                }
                var que = new Queue<Link>();
                que.Enqueue(new Link { description = good.name, url = $"~/Details/{good.groupId}/{good.id}" });
                que.Enqueue(new Link { description = good.groupName, url = "~/Store/Index/" + good.groupId });
                long? parentId = store.GetGroups().FirstOrDefault(s => s.id == good.groupId).parentId;
                while (parentId != null)
                {
                    var group = store.GetGroups().FirstOrDefault(s => s.id == parentId);
                    que.Enqueue(new Link { description = group.name, url = "~/Store/Index/" + group.id });
                    parentId = store.GetGroups().FirstOrDefault(s => s.id == group.parentId)?.parentId;
                }
                foreach (var item in que.Reverse())
                {
                    (ViewBag.Links as Queue<Link>).Enqueue(item);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                HttpContext.Response.Redirect("/Error/_404", true);
                return null;
            }
            
        }

        public ActionResult Index ()
        {
            return View();
        }
    }


}
