using ProductSystem.Models.Base;
using ProductSystem.Models.Dto;
using System.Collections.Generic;

namespace ProductSystem.Services.Interface
{
    public interface IProductService
    {
        APIResult Create(CreateProductDto product);
        APIResult Update(UpdateProductDto product);
        APIResult Delete(int id);
        IEnumerable<GetAllProductDto> GetAllProduct();
        IEnumerable<GetAllCategoryNameDto> GetAllCategoryName();
        bool IsProductNameExist(string name);
    }
}
