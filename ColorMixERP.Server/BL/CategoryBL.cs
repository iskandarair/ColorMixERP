using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;

namespace ColorMixERP.Server.BL
{
    public class CategoryBL
    {
        public List<Category> GetCategories()
        {
            return new CategoryDalFacade().GetCategories();
        }

        public Category GetCategory(int? id)
        {
            return new CategoryDalFacade().GetCategory(id);
        }

        public void Add(Category category)
        {
            new CategoryDalFacade().Add(category);
        }

        public void Update(Category category)
        {
            new CategoryDalFacade().Update(category);
        }

        public void Delete(int? id)
        {
            new CategoryDalFacade().Delete(id);
        }
    }
}
