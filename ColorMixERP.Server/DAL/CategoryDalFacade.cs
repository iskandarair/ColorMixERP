using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class CategoryDalFacade
    { 
        private LinqContext db = null;
        public CategoryDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<CategoryDTO> GetCategories(PaginationDTO cmd, ref int pagesCount)
        {
            var query = from c in db.Categories where c.IsDeleted == false select new CategoryDTO()
            {
                Id = c.Id ?? 0,
                Code = c.Code,
                Name = c.Name,
                IsDeleted = c.IsDeleted
            };
            var size = ((from p in query select p).Count() / cmd.PageSize);
            pagesCount = (int) Math.Ceiling((decimal)size);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public CategoryDTO GetCategory(int? id)
        {
            var categories = from c in db.Categories where c.IsDeleted == false && c.Id == id select new CategoryDTO()
            {
                Id = c.Id ?? 0,
                Code = c.Code,
                Name = c.Name,
                IsDeleted = c.IsDeleted
            };
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
            categoryToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int? id)
        {
            var element = (from c in db.Categories where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
