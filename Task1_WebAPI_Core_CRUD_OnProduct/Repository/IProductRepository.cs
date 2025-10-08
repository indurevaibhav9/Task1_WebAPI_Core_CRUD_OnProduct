using Task1_WebAPI_Core_CRUD_OnProduct.Models;

namespace Task1_WebAPI_Core_CRUD_OnProduct.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product GetById(int id);
        public Product UpdateById(int id, Product product);
        public Boolean DeleteById(int id);

        public int InsertProduct(Product product);
    }
}
