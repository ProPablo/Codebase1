using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Forge.Museum.Interfaces.DataTransferObjects.Artefact;
using Forge.Museum.Web.Models;
using Forge.Museum.BLL.Http;
using PagedList;


namespace Forge.Museum.Web.Controllers
{
    [Authorize]
    public class BeaconsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Beacons
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, string recentAction, string recentName, string recentId)
        {
            var request = new HTTPrequest();
            //Pass through most recent action details if redirected from an action
            if (recentAction != null && recentAction.Count() > 0)
            {
                ViewBag.Action = recentAction;
                ViewBag.RecentName = recentName;
                ViewBag.RecentId = recentId;
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            List<BeaconDto> beaconMasterList = await request.Get<List<BeaconDto>>("api/beacon?pageNumber=0&numPerPage=500&isDeleted=false");
            IEnumerable<BeaconDto> beaconsFiltered = beaconMasterList.ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                beaconsFiltered = beaconsFiltered.Where(a => a.Name.Contains(searchString));
            }


            var beacons = beaconsFiltered;

            switch (sortOrder)
            {
                case "id_desc":
                    beacons = beacons.OrderByDescending(a => a.Id);
                    break;
                case "Name":
                    beacons = beacons.OrderBy(a => a.Name);
                    break;
                case "name_desc":
                    beacons = beacons.OrderByDescending(a => a.Name);
                    break;
                default:
                    beacons = beacons.OrderBy(a => a.Id);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(beacons.ToPagedList(pageNumber, pageSize));
        }

        // GET: Beacons/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var request = new HTTPrequest();
            BeaconDto beacon = await request.Get<BeaconDto>("api/beacon/" + id);
            if (beacon == null)
            {
                return HttpNotFound();
            }
            return View(beacon);
        }

        // GET: Beacons/Create
        public async Task<ActionResult> Create()
        {
            var request = new HTTPrequest();
            return View();
        }

        // POST: Beacons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BeaconDto beacon)
        {
            // Checks Name is not Null or Empty
            if (string.IsNullOrEmpty(beacon.Name))
            {
                ViewBag.ValidationName = "Name field is required.";
                return View(beacon);
            }

            if (ModelState.IsValid && (beacon.Name != null))
            {
                var request = new HTTPrequest();
                beacon = await request.Post<BeaconDto>("api/beacon", beacon);
                return RedirectToAction("Index", new { recentAction = "Created", recentName = beacon.Name, recentId = beacon.Id });
            }
            else
            {
                var request = new HTTPrequest();
                ViewBag.Action = null;

                return View();
            }
        }

        // GET: Beacons/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var request = new HTTPrequest();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeaconDto beacon = await request.Get<BeaconDto>("api/beacon/" + id);
            if (beacon == null)
            {
                return HttpNotFound();
            }
            return View(beacon);
        }

        // POST: Beacons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BeaconDto beacon)
        {
            // Checks Name is not Null or Empty
            if (string.IsNullOrEmpty(beacon.Name))
            {
                ViewBag.ValidationName = "Name field is required.";
                return View(beacon);
            }

            var request = new HTTPrequest();
            if (ModelState.IsValid)
            {
                await request.Put<ArtefactCategoryDto>("api/beacon", beacon);
                return RedirectToAction("Index", new { recentAction = "Editted", recentName = beacon.Name, recentId = beacon.Id });
            }
            return View(beacon);
        }

        // GET: Beacons/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var request = new HTTPrequest();
            BeaconDto beacon = await request.Get<BeaconDto>("api/beacon/" + id);
            if (beacon == null)
            {
                return HttpNotFound();
            }
            return View(beacon);
        }

        // POST: Beacons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var request = new HTTPrequest();
                BeaconDto beacon = await request.Get<BeaconDto>("api/beacon/" + id);
                await request.Delete("api/beacon/" + id);
                return RedirectToAction("Index", new { recentAction = "Deleted", recentName = beacon.Name, recentId = beacon.Id });
            }
            catch (Exception)
            {
                throw;
            }
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
