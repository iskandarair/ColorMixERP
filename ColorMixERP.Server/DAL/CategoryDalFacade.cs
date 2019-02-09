using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Server.DAL
{
    public class CategoryDalFacade
    { 
        private LinqContext db = null;
        public CategoryDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<Category> GetCategories()
        {
            var query = from c in db.Categories select c;
            return query.ToList();
        }

        public Category GetCategory(int? id)
        {
            var categories = GetCategories();
            return categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Add(Category category)
        {
            Category categoryToinsert = new Category();
            categoryToinsert = category;
            db.Categories.InsertOnSubmit(categoryToinsert);
            db.SubmitChanges();
        }

        public void Update(Category category)
        {
            var categoryToUpdate = GetCategory(category.Id);
            categoryToUpdate.Code = category.Code;
            categoryToUpdate.Name = category.Name;
            db.SubmitChanges();
        }

        public void Delete(int? id)
        {
            var categoryToDelete = GetCategory(id);
            db.Categories.DeleteOnSubmit(categoryToDelete);
            db.SubmitChanges();
        }
    }
}
