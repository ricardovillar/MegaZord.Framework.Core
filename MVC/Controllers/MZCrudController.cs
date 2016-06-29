using System;
using System.Linq;
using MegaZord.Framework.MVC.Filter;
using MegaZord.Framework.Models;
using MegaZord.Framework.Helpers;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNet;

namespace MegaZord.Framework.MVC.Controllers {
    public abstract class LMCrudController<TDatabaseContext, TEntity> : MZBaseController
        where TEntity : MZDBBaseEntity
        where TDatabaseContext : DbContext {


        protected abstract Expression<Func<TEntity, bool>> GetWherePredicate(string value);
        protected virtual IQueryable<TEntity> AlterQueryableToExecute(IQueryable<TEntity> queryable) {
            return queryable;
        }
        protected virtual void OnBeforeRenderCreateUpdate(TEntity entity) { }
        protected virtual void OnBeforeSubmitChanges(TEntity entity) { }
        protected virtual string SortOderDefault() {
            return "Id";
        }

        [HttpGet, LMImportModelStateFromTempData]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page) {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }
            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = SortOderDefault();

            ViewBag.CurrentFilter = searchString;

            IPagedList<TEntity> entityList = null;
            using (var db = Activator.CreateInstance<TDatabaseContext>()) {
                var pageSize = MZHelperConfiguration.MZCRUDPageSize;
                int pageNumber = (page ?? 1);
                var iqueryable = db.Set<TEntity>().AsQueryable();
                iqueryable = AlterQueryableToExecute(iqueryable);

                if (!string.IsNullOrEmpty(ViewBag.CurrentFilter)) {
                    Expression<Func<TEntity, bool>> whereFilter = GetWherePredicate(ViewBag.CurrentFilter);
                    iqueryable = iqueryable.Where(whereFilter);
                }
                //aplica o sort
                iqueryable = iqueryable.OrderBy(sortOrder);

                //executa a query
                entityList = iqueryable.ToPagedList(pageNumber, (int)pageSize);
            }
            return View("Index", entityList);
        }


        protected virtual void ExecuteDelete(int id) {
            using (var db = Activator.CreateInstance<TDatabaseContext>()) {
                var entity = db.Set<TEntity>().FirstOrDefault(x => x.Id == id);
                if (entity != null) {
                    db.Set<TEntity>().Remove(entity);
                    db.SaveChanges();
                }
            }
        }
        [HttpGet, LMExportModelStateToTempData]
        public ActionResult Delete(int id) {
            ExecuteDelete(id);
            return RedirectToAction("Index");
        }

        [HttpGet, LMExportModelStateToTempData]
        public ActionResult CreateUpdate(int? id) {
            using (var db = Activator.CreateInstance<TDatabaseContext>()) {
                TEntity entity = null;
                if (id.HasValue)
                    entity = db.Set<TEntity>().FirstOrDefault(x => x.Id == id);
                OnBeforeRenderCreateUpdate(entity);
                return View(entity);
            }
        }

        [HttpPost, LMExportModelStateToTempData]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(TEntity entity) {
            if (ModelState.IsValid) {
                using (var db = Activator.CreateInstance<TDatabaseContext>()) {
                    OnBeforeSubmitChanges(entity);
                    if (entity.Id == 0) {
                        db.Set<TEntity>().Add(entity);
                    }
                    else {
                        db.Set<TEntity>().Attach(entity);
                        db.Entry(entity).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    //CASTRO
                    //db.Entry<TEntity>(entity).Reload();
                }
                return RedirectToAction("Index");
            }

            return View(entity);
        }


    }
}
