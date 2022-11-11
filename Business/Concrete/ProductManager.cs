using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    { IProductDal  _productDal;
        ICategoryService _categoryservice; 
        
        
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
           _categoryservice = categoryService;  
            //_logger = logger;
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)

        {
            IResult result = BusinessRules.Run(CheckIfProductNameİsSame(product.ProductName),
                 CheckIfProductkCountOfCategoryCorrect(product.CategoryID), CheckIfThereAreFifteenCategory());   

            if(result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);


            //if (CheckIfProductkCountOfCategoryCorrect(product.CategoryID).Success)
            //{
            //    if (CheckIfProductNameİsSame(product.ProductName).Success)
            //    {
                   

            //    }
               

            //}

            //return new ErrorResult();
            //_logger.log();
            //try
            //{
            //    _logger.log();
            //try
            //{
              
            //}
            //catch (Exception exception)
            //{

            //    _logger.log();

            //}
            //return new ErrorResult();
            // business.codes..../ burya yazılacak iş kodları
            // Validation kodu : dogrulama kodu valıd ınvalıd
            //if (product.UnitPrice <= 0)
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}

            //if (product.ProductName.Length < 2)
            //{
            //    // magic strings
            //    return new ErrorResult(Messages.ProductNameInValid);
            //}

            

            //ValidationTool.Validate(new ProductValidator(), product);
            
            // loglama 
            //cacheremove
            // performance
            // transaction
            // yetkilendirme


            // business codes

            
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 3)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);    
            }


            //işkodları
            //yetkisi var mı ? 
            // InMemoryProductDal inMemoryProductDal = new InMemoryProductDal();
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed); 

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryID == id));
        }

        public IDataResult <List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
           return new SuccessDataResult<List<Product>>( _productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product> (_productDal.Get(p => p.ProductID == productId));

        }

        public IDataResult <List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>( _productDal.GetProductDetails());
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        // Select count (*) from product where categoryId=1
        // **_productDal.GetAll(p => p.CategoryID == categoryId).Count;
        private IResult CheckIfProductkCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryID == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameİsSame(String productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if(result  )
            {
                return new ErrorResult(Messages.ProductNameError);
                
            }
            return new SuccessResult(Messages.ProductAdded);
        }

        //private IResult CheckIfThereAreFifteenCategory(int category)
        //{
        //    var result = _categoryservice.GetById<categoryID>(int p => p.categoryID == categoryID);    
        //    if (result >= 15)
        //    {
        //        return new ErrorResult(Messages.MoreThanFifteen);
        //    }
        //    return new SuccessResult(Messages.ProductAdded);
        //}


        private IResult CheckIfThereAreFifteenCategory()
        {
            var result = _categoryservice.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.MoreThanFifteen);
            }
            return new SuccessResult(Messages.ProductAdded);
        }

    }
}
