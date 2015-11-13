using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEx1.Models;

namespace MVCEx1.Controllers
{
	public class 客戶資料ViewController : Controller
	{
		private CustomerEntities db = new CustomerEntities();

		// GET: 客戶資料View
		public ActionResult Index()
		{
			return View(db.客戶資料View.ToList());
		}

		[HttpPost]
		public ActionResult Index(string searchStr)
		{
			ViewBag.QueryString = string.Empty;
			if (!string.IsNullOrEmpty(searchStr))
			{
				ViewBag.QueryString = searchStr;

				var result = db.客戶資料View.Where(
					x => x.客戶名稱.
					ToLower().Contains(searchStr.ToLower())
					||
					x.Email.ToLower().Contains(searchStr.ToLower())	
					||
					x.電話.ToLower().Contains(searchStr.ToLower())
					||
					x.統一編號.ToLower().Contains(searchStr.ToLower())
					||
					x.地址.ToLower().Contains(searchStr.ToLower())
					);
				return View(result);

			}
			return View(new List<客戶資料View>());

		}

		// GET: 客戶資料View/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶資料View 客戶資料View = db.客戶資料View.Find(id);
			if (客戶資料View == null)
			{
				return HttpNotFound();
			}
			return View(客戶資料View);
		}

		// GET: 客戶資料View/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: 客戶資料View/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除註記,聯絡人數量,銀行帳戶數量")] 客戶資料View 客戶資料View)
		{
			if (ModelState.IsValid)
			{
				db.客戶資料View.Add(客戶資料View);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(客戶資料View);
		}

		// GET: 客戶資料View/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶資料View 客戶資料View = db.客戶資料View.Find(id);
			if (客戶資料View == null)
			{
				return HttpNotFound();
			}
			return View(客戶資料View);
		}

		// POST: 客戶資料View/Edit/5
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除註記,聯絡人數量,銀行帳戶數量")] 客戶資料View 客戶資料View)
		{
			if (ModelState.IsValid)
			{
				db.Entry(客戶資料View).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(客戶資料View);
		}

		// GET: 客戶資料View/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶資料View 客戶資料View = db.客戶資料View.Find(id);
			if (客戶資料View == null)
			{
				return HttpNotFound();
			}
			return View(客戶資料View);
		}

		// POST: 客戶資料View/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			客戶資料View 客戶資料View = db.客戶資料View.Find(id);
			db.客戶資料View.Remove(客戶資料View);
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
