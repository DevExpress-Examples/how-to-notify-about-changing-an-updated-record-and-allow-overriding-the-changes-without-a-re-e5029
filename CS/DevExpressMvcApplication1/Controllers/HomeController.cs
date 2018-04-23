using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DevExpressMvcApplication1.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			ViewBag.Message = "Welcome to DevExpress Extensions for ASP.NET MVC!";

			return View(NorthwindDataProvider.GetProducts());
		}

		public ActionResult InlineEditingPartial() {
			return PartialView("GridView", NorthwindDataProvider.GetEditableProducts());
		}

		[HttpPost]
		public ActionResult InlineEditingUpdatePartial([ModelBinder(typeof(DevExpressEditorsBinder))] EditableProduct product, bool forceUpdate = false) {
			if (ModelState.IsValid) {
				try {
					bool dbCheckResult = false; //checking is failed, for demo only

					bool canUpdate = dbCheckResult || forceUpdate;
					ViewData["canUpdate"] = canUpdate;
					if (canUpdate) {
						//Update is not allowed in online demos
						//NorthwindDataProvider.UpdateProduct(product);
					}
					else
						ViewData["editedRowKey"] = product.ProductID;
				}
				catch (Exception e) {
					ViewData["EditError"] = e.Message;
				}
			}
			else
				ViewData["EditError"] = "Please, correct all errors.";

			return PartialView("GridView", NorthwindDataProvider.GetEditableProducts());
		}

	}
}
