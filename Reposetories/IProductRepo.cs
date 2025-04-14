namespace API_1.Reposetories
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        Product GetByName(string name);

        void CreateProduct(Product product );

        void Update(Product product );

        void Delete(int id);

        void Save();


    }
}
