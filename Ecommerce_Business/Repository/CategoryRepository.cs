using AutoMapper;
using Ecommerce_Business.Repository.IRepository;
using Ecommerce_DataAccess;
using Ecommerce_DataAccess.Data;
using Ecommerce_Models;

namespace Ecommerce_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public CategoryDTO Create(CategoryDTO objDTO) //CategoryDTO must be accessed by converting to its corresponding category object
        {
            // convert from CategoryDTO to Category with source = objDTO
            var obj = _mapper.Map<CategoryDTO, Category>(objDTO);

            //this will add the category to the database
            var addedObj= _db.Categories.Add(obj);

            //this method must be used otherwise the data will not be stored in database
            _db.SaveChanges(); 

            return _mapper.Map<Category, CategoryDTO>(addedObj.Entity); //source is addedObj
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
