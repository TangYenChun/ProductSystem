using ProductSystem.Models.Base;
using ProductSystem.Models.Dto;

namespace ProductSystem.Services.Interface
{
    public interface IProductService
    {
        APIResult Create(CreateProductDto product);
        APIResult Update(UpdateProductDto product);
        APIResult Delete(int id);
        APIResult GetAllProduct();
        APIResult GetAllCategoryName();
        bool IsProductNameExist(string name);
    }
}
