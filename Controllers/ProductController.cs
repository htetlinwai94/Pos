using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mysqlefcore;

namespace PosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ProductInfo Get(string? title,int pageno,int limit)
        {
            
            return getData(title,pageno,limit);
            
        }

        [HttpPost]
        public object Post(Product _product)
        {
            try{
            InsertData(_product);
            return true;
            }catch(Exception e){
                
                return e;
            }

        }
        
        [HttpPut]
        public object Put(Product _product)
        {
            try{
            UpdateData(_product);
            return true;
            }catch(Exception e){
                
                return e;
            }

        }

        [HttpDelete]
        public object Delete(Product _product)
        {
            try{
            DeleteData(_product);
            return true;
            }catch(Exception e){
                
                return e;
            }

        }
        private static void UpdateData(Product _product)
        {
            using (var context = new PosContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();
                var _updateProduct =context.Product.Where(p => p.PCode == _product.PCode).First();
                _updateProduct.Name= _product.Name;


                

               // Saves changes
                context.SaveChanges();
            }
        }  


        private static void DeleteData(Product _product)
        {
            using (var context = new PosContext())
            {
                // Creates the database if not exists
               context.Database.EnsureCreated();
               var _deleteProduct =context.Product.Where(p=> p.PCode == _product.PCode).First();
               context.Product.Remove(_deleteProduct);


                

               // Saves changes
                context.SaveChanges();
            }
        }  


        private static void InsertData(Product _product)
        {
            using (var context = new PosContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();
               context.Product.Add(_product);
               context.SaveChanges();
            }
        }

        



         private static ProductInfo getData(string? title, int pageno, int limit)
    {
      // Gets and prints all books in database
      using (var context = new PosContext())
      {
           context.Database.EnsureCreated();
        var _query = context.Product.Where(p=>p.Name.StartsWith(title) || title== null);
        var _ProductInfo = new ProductInfo();
        _ProductInfo.total = _query.Count();
        int _skipCount = (pageno - 1) * limit;
        _ProductInfo.skip = _skipCount;
        _ProductInfo.limit = limit;
        _ProductInfo.products = _query.Skip(_skipCount).Take(limit).ToList();
        return _ProductInfo;
       
      }
    }
    }

    public class ProductInfo {
        public int total{get;set;}
         public int skip{get;set;}
          public int limit{get;set;}

        public IEnumerable<Product> products{get;set;}
    }
}
