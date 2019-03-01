using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class CategoryBL
    {
        public List<CategoryDTO> GetCategories(PaginationDTO cmd, ref int pagesCount)
        {
            return new CategoryDalFacade().GetCategories(cmd, ref pagesCount);
        }

        public CategoryDTO GetCategory(int? id)
        {
            return new CategoryDalFacade().GetCategory(id);
        }

        public void Add(CategoryDTO category)
        {
            new CategoryDalFacade().Add(category);
        }

        public void Update(CategoryDTO category)
        {
            new CategoryDalFacade().Update(category);
        }

        public void Delete(int? id)
        {
            new CategoryDalFacade().Delete(id);
        }
    }
}
