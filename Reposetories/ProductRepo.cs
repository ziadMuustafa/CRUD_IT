
using API_1.MyAppContext;
using Microsoft.EntityFrameworkCore;

namespace API_1.Reposetories
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            _context.Products.Remove(_context.Products.FirstOrDefault(en => en.Id == id));
            _context.SaveChanges(); 
        }

        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> products = _context.Products.ToList(); 
            return products;

        }

        public Product GetById(int id)
        {
            Product? product = _context.Products.FirstOrDefault(en => en.Id == id);
            return product;
        }

        public void Save()
        {
            _context.SaveChanges();

        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }


        public Product GetByName(string name)
        {
            Product? product = _context.Products.FirstOrDefault(en => en.Name == name);
            return product;
        }
    }
}
