using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.DAL
{
    public class CategoryDalFacade
    { 
        private LinqContext db = null;
        public CategoryDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<CategoryDTO> GetCategories()
        {
            var query = from c in db.Categories where c.IsDeleted == false select new CategoryDTO()
            {
                Id = c.Id ?? 0,
                Code = c.Code,
                Name = c.Name
            };
            return query.ToList();
        }

        public CategoryDTO GetCategory(int? id)
        {
            var categories = GetCategories();
            return categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Add(CategoryDTO category)
        {
            Category categoryToinsert = new Category()
            {
                Code =  category.Code,
                Name = category.Name
            };
            db.Categories.InsertOnSubmit(categoryToinsert);
            db.SubmitChanges();
        }

        public void Update(CategoryDTO category)
        {
            var categoryToUpdate = (from c in db.Categories where c.Id == category.Id select c).FirstOrDefault();
            categoryToUpdate.Code = category.Code;
            categoryToUpdate.Name = category.Name;
            db.SubmitChanges();
        }

        public void Delete(int? id)
        {
            var element = (from c in db.Categories where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            db.SubmitChanges();
        }
    }
}
