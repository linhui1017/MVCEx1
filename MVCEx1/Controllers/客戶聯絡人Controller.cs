﻿using System;
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
	public class 客戶聯絡人Controller : Controller
	{
		private CustomerEntities db = new CustomerEntities();

		// GET: 客戶聯絡人
		public ActionResult Index()
		{
			var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
			return View(客戶聯絡人.ToList());
		}
		[HttpPost]
		public ActionResult IndexByClient(string searchStr, int id)
		{
			IQueryable<客戶聯絡人> result = null;
			ViewBag.Title = string.Empty;
			客戶資料 client = null;

			ViewBag.QueryString = string.Empty;

			if (id >= 0)
			{
				client = db.客戶資料.Find(id);
			}
			if (null != client)
			{

				ViewBag.Title = (null == client ? string.Empty : client.客戶名稱);
				ViewBag.客戶Id = id;
				ViewBag.QueryString = searchStr;

				result = db.客戶聯絡人.Where(
					x => x.客戶Id == id
					&&  x.刪除註記 == false
					&& (x.姓名.ToLower().Contains(searchStr)
						|| x.Email.ToLower().Contains(searchStr)
						|| x.手機.ToLower().Contains(searchStr)
					)
					&& x.刪除註記 == false);

			}
			else
			{
				return RedirectToAction("Index", "客戶資料View");
			}
			ViewBag.Title = string.Format("{0}{1}{2}"
				, ViewBag.Title
				, (string.IsNullOrEmpty(ViewBag.Title) ? "" : "--")
				, "聯絡人資料");

			return View(result.ToList());



		}
		// GET: 客戶聯絡人
		//依照客戶ID找出連絡人清單資料
		public ActionResult IndexByClient(int? id)
		{
			IQueryable<客戶聯絡人> result = null;
			ViewBag.Title = string.Empty;
			客戶資料 client = null;

			if (null != id) {
				client = db.客戶資料.Find(id);
			}
			if (null != client) 
			{

				ViewBag.Title = (null == client ? string.Empty : client.客戶名稱);
				ViewBag.客戶Id = id;
				result = db.客戶聯絡人.Where(x => x.客戶Id == id && x.刪除註記 == false);

			}
			else
			{
				return RedirectToAction("Index", "客戶資料View");
			}
			ViewBag.Title = string.Format("{0}{1}{2}"
				,ViewBag.Title
				, (string.IsNullOrEmpty(ViewBag.Title) ? "" : "--")
				,"聯絡人資料");

			return View(result.ToList());
		}

		// GET: 客戶聯絡人/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			if (客戶聯絡人 == null)
			{
				return HttpNotFound();
			}
			return View(客戶聯絡人);
		}

		// GET: 客戶聯絡人/Create
		public ActionResult Create()
		{
			ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
			return View();
		}

		// GET: 客戶聯絡人/Create
		public ActionResult CreateByClient(int? id)
		{
			ViewBag.客戶Id = new SelectList(db.客戶資料.Where(x=>x.Id == id), "Id", "客戶名稱");
			ViewBag.ClientID = id;
			return View();
		}

		// POST: 客戶聯絡人/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateByClient(客戶聯絡人 客戶聯絡人)
		{
			
			if (ModelState.IsValid)
			{
				db.客戶聯絡人.Add(客戶聯絡人);
				db.SaveChanges();
				return RedirectToAction("IndexByClient", "客戶聯絡人", new { id = 客戶聯絡人.客戶Id });
			}
			 

			ViewBag.客戶Id = new SelectList(db.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
			return View(客戶聯絡人);
		}

		// POST: 客戶聯絡人/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,刪除註記")] 客戶聯絡人 客戶聯絡人)
		{
			if (ModelState.IsValid)
			{
				db.客戶聯絡人.Add(客戶聯絡人);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
			return View(客戶聯絡人);
		}

		// GET: 客戶聯絡人/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			if (客戶聯絡人 == null)
			{
				return HttpNotFound();
			}
			ViewBag.客戶Id = new SelectList(db.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
			return View(客戶聯絡人);
		}

		// POST: 客戶聯絡人/Edit/5
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,刪除註記")] 客戶聯絡人 客戶聯絡人)
		{
			if (ModelState.IsValid)
			{
				db.Entry(客戶聯絡人).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("IndexByClient", "客戶聯絡人", new { id = 客戶聯絡人.客戶Id });
			}
			ViewBag.客戶Id = new SelectList(db.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
			return View(客戶聯絡人);
		}

		// GET: 客戶聯絡人/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			if (客戶聯絡人 == null)
			{
				return HttpNotFound();
			}
			return View(客戶聯絡人);
		}

		// POST: 客戶聯絡人/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			//db.客戶聯絡人.Remove(客戶聯絡人);
			客戶聯絡人.刪除註記 = true;
			db.SaveChanges();
			return RedirectToAction("IndexByClient", "客戶聯絡人", new { id = 客戶聯絡人.客戶Id });
			//return RedirectToAction("Index");
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
