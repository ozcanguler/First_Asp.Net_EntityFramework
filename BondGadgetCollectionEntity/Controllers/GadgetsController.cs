using BondGadgetCollectionEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BondGadgetCollectionEntity.Controllers
{
    public class GadgetsController : Controller
    {
        private ApplicationDbContext context;
        public GadgetsController()
        {
            context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing) //referance to this database context.
        {
            context.Dispose();
        }
        // GET: Gadgets
        public ActionResult Index()
        {
            List<GadgetModel> gadgets = context.Gadgets.ToList();
            return View("Index",gadgets);
        }
        public ActionResult Details(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g=>g.Id==id); //Anonymous Function
            return View("Details",gadget);
        }
        public ActionResult ProcessCreate(GadgetModel gadgetModel)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.Id == gadgetModel.Id);
            //save to the db

            //edit
            if (gadget != null)
            {
                gadget.Name = gadgetModel.Name;
                gadget.Description = gadgetModel.Description;
                gadget.AppearsIn = gadgetModel.AppearsIn;
                gadget.WithThisActor = gadgetModel.WithThisActor;
            }
            //new item
            else
            {
                context.Gadgets.Add(gadgetModel);
               
            }
            context.SaveChanges();
            return View("Details", gadgetModel);

        }
        public ActionResult Create()
        {
            return View("GadgetForm",new GadgetModel());
        }
        public ActionResult Edit(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.Id == id);
            return View("GadgetForm",gadget);
        }

        public ActionResult Delete(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.Id == id); //Anonymous Function

            context.Entry(gadget).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
            return Redirect("/Gadgets");
        }
        public ActionResult SearchForm()
        {

            return View("SearchForm");
        }
        public ActionResult SearchForName(string searchPhrase)
        {
            //get a list of search results from the database

            var gadgets = from g in context.Gadgets where g.Name.Contains(searchPhrase) select g;
            return View("Index",gadgets);
        }
    }
}