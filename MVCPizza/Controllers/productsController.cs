using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCPizza.Controllers
{
    public class productsController : Controller
    {
        private PizzaEntities db = new PizzaEntities();
		
		// KF added ExecAction-Method
		public ActionResult ExecAction(string id)
        {
            var pa = new productsActions();
            pa.ExecAction(id, db);
            return RedirectToAction("Index");
        }
        
		// GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.category);
			ViewBag.MenuAdd = products.FirstOrDefault().actions;
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.category, "categoryid", "categoryname");
            return View();
        }

        // POST: products/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,categoryid,productcode,productname,picturefile,productprice,barcode,supplierid,timestamp_column,zusaetze")] products products)
        {
            if (ModelState.IsValid)
            {

   				// KF added Hooks
                if (products is IControllerHooks)
                {
                    ((IControllerHooks)products).OnAdd();
                    if (products.ErrorMessages != null
                        && products.ErrorMessages.Count > 0)
                    {
                        ViewBag.MessageList = products.ErrorMessages;
                        ViewBag.categoryid = new SelectList(db.category, "categoryid", "categoryname");
                        return View(products);
                    }
                }

                db.products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryid = new SelectList(db.category, "categoryid", "categoryname", products.categoryid);
            return View(products);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.category, "categoryid", "categoryname", products.categoryid);
            return View(products);
        }

        // POST: products/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productid,categoryid,productcode,productname,picturefile,productprice,barcode,supplierid,timestamp_column,zusaetze")] products products, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
				// KF added Hooks
                if (products is IControllerHooks)
                {
                    ((IControllerHooks)products).OnUpdate();
                    if (products.ErrorMessages != null 
                        && products.ErrorMessages.Count > 0)
                    {
                        ViewBag.MessageList = products.ErrorMessages; 
                        ViewBag.categoryid = new SelectList(db.category, "categoryid", "categoryname");
						return View(products);
					}
                }
                // added:                
                if (uploadFile != null)
                {
                    // store fileNAME in model:
                    products.picturefile = Path.GetFileName(uploadFile.FileName);

                    // upload and save stream on server
                    string FilePath = HttpContext.Server.MapPath("~/Images/" + products.picturefile);
                    uploadFile.SaveAs(FilePath);
                }
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryid = new SelectList(db.category, "categoryid", "categoryname", products.categoryid);
            return View(products);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products products = db.products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            products products = db.products.Find(id);
            db.products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
