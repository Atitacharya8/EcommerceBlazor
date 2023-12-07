using Ecommerce_Business.Repository.IRepository;
using Ecommerce_DataAccess;
using Ecommerce_DataAccess.Data;
using Ecommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public CategoryDTO Create(CategoryDTO objDTO) //CategoryDTO must be accessed by converting to its corresponding category object
        {
            Category category = new Category()
            {
                Name = objDTO.Name,
                Id = objDTO.Id,
                CreatedDate = DateTime.Now,
            };
            _db.Categories.Add(category); //this will add the category to the database
            _db.SaveChanges(); //this method must be used otherwise the data will not be stored in database

            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryDTO Update(CategoryDTO objDTO)
        {
            throw new NotImplementedException();
        }
    }
}
