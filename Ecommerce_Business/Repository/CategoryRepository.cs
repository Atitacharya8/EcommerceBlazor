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

            obj.CreatedDate = DateTime.Now;

            //this will add the category to the database
            var addedObj= _db.Categories.Add(obj);

            //this method must be used otherwise the data will not be stored in database
            _db.SaveChanges(); 

            return _mapper.Map<Category, CategoryDTO>(addedObj.Entity); //source is addedObj
        }

        public int Delete(int id)
        {
            var obj = _db.Categories.FirstOrDefault(c=>c.Id == id);
            if(obj != null)
            {
                _db.Categories.Remove(obj);
                return _db.SaveChanges();
            }
            return 0;
        }

        public CategoryDTO Get(int id)
        {
            var obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (obj != null)
            {
                
             return _mapper.Map<Category, CategoryDTO>(obj);
            }

            //if null return new category DTO
            return new CategoryDTO();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories); //source will be retrieved using _db.Categories
        }

        public CategoryDTO Update(CategoryDTO objDTO)
        {
            var objFromDb = _db.Categories.FirstOrDefault(c => c.Id == objDTO.Id);
            if( objFromDb != null )
            {
                objFromDb.Name=objDTO.Name; //we only use name here so id and createddate are not used
                _db.Categories.Update(objFromDb);
                _db.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(objFromDb);
                
            }
            return objDTO; //if something is not valid the original name will be returned
        }
    }
}