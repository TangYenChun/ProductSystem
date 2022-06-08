using ProductSystem.Models;
using ProductSystem.Models.Base;
using ProductSystem.Models.Dto;
using ProductSystem.Repositories.Interface;
using ProductSystem.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        public ProductService(IRepository repository)
        {
            _repository = repository;
        }
        public APIResult Create(CreateProductDto product)
        {
            //檢核
            if (IsProductNameExist(product.ProductName))
            {
                return new APIResult(APIStatus.Fail, "此名稱已存在", null);
            }

            //取得種類Id
            var categoryId = _repository.GetAll<Category>().First(x => x.CategoryName == product.CategoryName).CategoryId;

            var entity = new Product()
            {
                ProductName = product.ProductName,
                CategoryId = categoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Discontinued = product.Discontinued
            };

            try
            {
                _repository.Create(entity);
                _repository.Save();
                return new APIResult(APIStatus.Success, string.Empty, null);
            }
            catch(Exception ex)
            {
                return new APIResult(APIStatus.Fail, ex.Message, null);
            }
        }

        public APIResult Delete(int id)
        {
            var entity = _repository.GetAll<Product>().First(x => x.ProductId == id);
            try
            {
                _repository.Delete(entity);
                _repository.Save();
                return new APIResult(APIStatus.Success, string.Empty, null);
            }
            catch (Exception ex)
            {
                return new APIResult(APIStatus.Fail, ex.ToString(), null);
            }
        }

        public IEnumerable<GetAllCategoryNameDto> GetAllCategoryName()
        {
            return _repository.GetAll<Category>().Select(x => new GetAllCategoryNameDto
            {
                CategoryName = x.CategoryName
            }).ToList();
            
        }

        public IEnumerable<GetAllProductDto> GetAllProduct()
        {
            return _repository.GetAll<Product>().Select(x => new GetAllProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                CategoryName = _repository.GetAll<Category>().First(y => y.CategoryId == x.CategoryId).CategoryName,
                QuantityPerUnit = x.QuantityPerUnit,
                UnitPrice = (decimal)x.UnitPrice,
                UnitsInStock = (short)x.UnitsInStock,
                Discontinued = x.Discontinued
            });
        }

        public bool IsProductNameExist(string name)
        {
            return _repository.GetAll<Product>().Any(x => x.ProductName == name);
        }

        public APIResult Update(UpdateProductDto product)
        {
            var entity = _repository.GetAll<Product>().First(x => x.ProductId == product.ProductId);
            entity.ProductName = product.ProductName;
            entity.CategoryId = _repository.GetAll<Category>().First(x => x.CategoryName == product.CategoryName).CategoryId;
            entity.QuantityPerUnit = product.QuantityPerUnit;
            entity.UnitPrice = product.UnitPrice;
            entity.UnitsInStock = product.UnitsInStock;
            entity.Discontinued = product.Discontinued;

            try
            {
                _repository.Update(entity);
                _repository.Save();
                return new APIResult(APIStatus.Success, string.Empty, null);
            }
            catch (Exception ex)
            {
                return new APIResult(APIStatus.Fail, ex.ToString(), null);
            }
        }
    }
}
