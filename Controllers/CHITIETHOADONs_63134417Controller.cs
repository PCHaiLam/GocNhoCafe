﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CafeGocNho_63134417.Models;
using Microsoft.Ajax.Utilities;

namespace CafeGocNho_63134417.Controllers
{
    public class CHITIETHOADONs_63134417Controller : Controller
    {
        private CafeGocNho_63134417Entities db = new CafeGocNho_63134417Entities();

        // GET: CHITIETHOADONs_63134417
        public ActionResult Index(string billId = null, string tableId = null, int discount = 0)
        {
            var cthd = db.CHITIETHOADON
                        .Where(c => c.HOADON.MAHD == billId)
                        .Include(c => c.MENU)
                        .Include(c => c.HOADON)
                        .GroupBy(c => new { c.MAMH }) 
                        .ToList();

            var hoaDon = db.HOADON.FirstOrDefault(h => h.MAHD == billId && h.MABAN.ToString() == tableId);

            int total = (int)hoaDon.CHITIETHOADON.Sum(h => h.SOLUONG * h.MENU.GIACA); 

            decimal discountAmount = total * (discount / 100.0m);
            decimal afterDiscount = total - discountAmount;

            ViewBag.total = total.ToString("N0");           
            ViewBag.discount = discount;                    
            ViewBag.discountAmount = discountAmount.ToString("N0");
            ViewBag.afterDiscount = afterDiscount.ToString("N0");

            ViewBag.billId = billId;
            ViewBag.tableId = tableId;

            return View(db.CHITIETHOADON.ToList());

        }
        // GET: CHITIETHOADONs_63134417/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADON.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADONs_63134417/Create
        public ActionResult Create()
        {
            ViewBag.MAMH = new SelectList(db.MENU, "MAMH", "TENMH");
            ViewBag.MAHD = new SelectList(db.HOADON, "MAHD", "MANV");
            return View();
        }

        // POST: CHITIETHOADONs_63134417/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAHD,MAMH,SOLUONG")] CHITIETHOADON cHITIETHOADON)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETHOADON.Add(cHITIETHOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAMH = new SelectList(db.MENU, "MAMH", "TENMH", cHITIETHOADON.MAMH);
            ViewBag.MAHD = new SelectList(db.HOADON, "MAHD", "MANV", cHITIETHOADON.MAHD);
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADONs_63134417/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADON.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAMH = new SelectList(db.MENU, "MAMH", "TENMH", cHITIETHOADON.MAMH);
            ViewBag.MAHD = new SelectList(db.HOADON, "MAHD", "MANV", cHITIETHOADON.MAHD);
            return View(cHITIETHOADON);
        }

        // POST: CHITIETHOADONs_63134417/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHD,MAMH,SOLUONG")] CHITIETHOADON cHITIETHOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETHOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAMH = new SelectList(db.MENU, "MAMH", "TENMH", cHITIETHOADON.MAMH);
            ViewBag.MAHD = new SelectList(db.HOADON, "MAHD", "MANV", cHITIETHOADON.MAHD);
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADONs_63134417/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADON.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHOADON);
        }

        // POST: CHITIETHOADONs_63134417/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADON.Find(id);
            db.CHITIETHOADON.Remove(cHITIETHOADON);
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
