using AutoMapper;
using Ecommerce_Business.Repository.IRepository;
using Ecommerce_DataAccess;
using Ecommerce_DataAccess.Data;
using Ecommerce_Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task<ProductDTO> Create(ProductDTO objDTO) //ProductDTO must be accessed by converting to its corresponding category object
        {
            // convert from ProductDTO to Product with source = objDTO
            var obj = _mapper.Map<ProductDTO, Product>(objDTO);

            //this will add the category to the database
            var addedObj = _db.Products.Add(obj);

            //this method must be used otherwise the data will not be stored in database
            await _db.SaveChangesAsync();

            //convert from product to productdto to display
            return _mapper.Map<Product, ProductDTO>(addedObj.Entity); //source is addedObj
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _db.Products.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                //should get data by mapping from product source to productdto destination
                return _mapper.Map<Product, ProductDTO>(obj);
            }

            //if null return new category DTO
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            //should get list of data by mapping from product source to productdto destination
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products); //source will be retrieved using _db.Categories
        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var objFromDb = await _db.Products.FirstOrDefaultAsync(c => c.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Description = objDTO.Description;
                objFromDb.ShopFavorites = objDTO.ShopFavorites;
                objFromDb.CustomerFavorites = objDTO.CustomerFavorites;
                objFromDb.Color = objDTO.Color;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.CategoryId = objDTO.CategoryId;
                _db.Products.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(objFromDb);

            }
            return objDTO; //if something is not valid the original name will be returned
        }
    }
}
