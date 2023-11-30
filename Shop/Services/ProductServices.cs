using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dto;
using Shop.Entities;
using System.Linq.Expressions;

namespace Shop.Services
{
    public class ProductServices
    {
        private readonly DataContext context;
        

        public ProductServices(DataContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await context.Products.ToListAsync();
            return products;
        }


        //Pagination- special part where you can find by filter name
        //or just make more easier to look by separate to small groups
        //filter using for separate only needed products
        //SortByCanAccept Only - Price and Name
        //sortByDesceding accept only false and bool

        public async Task<ActionResult<PagedResult<Product>>> GetProductsByPage(string filter,string sortBy,bool sortByDesceding)
        {
            
            int pageNumber = 1;
            int pageSize = 10;

            var query = context.Products
            .Where(u => filter == null 
            || (u.Name.ToLower().Contains(filter.ToLower()) 
            || u.Description.ToLower().Contains(filter.ToLower())));

            var totalCount = query.Count();

            if (sortBy != null)
            {
                var columnSelector = new Dictionary<string, Expression<Func<Product, object>>>
        {
            {"Name", product => product.Name },
            {"Price", product => product.Price }
        };

                var sortByExpresion = columnSelector[sortBy];
                query = sortByDesceding
                ? query.OrderByDescending(sortByExpresion)
                : query.OrderBy(sortByExpresion);

            }

            var result = query.Skip(pageSize * (pageNumber - 1))
               .Take(pageSize)
               .ToList();
            var pagedResult = new PagedResult<Product>(result, totalCount, pageSize, pageNumber);
            return pagedResult;
        }



        public async Task<ActionResult<Product>> CreateLot(Product productdto)
        {
            string photo = CheckPhoto(productdto.PhotoUrl);
            var product = new Product
            {
                Name = productdto.Name,
                Description = productdto.Description,
                PhotoUrl = photo,
                Price = productdto.Price
            };

            await context.AddAsync(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task Deletelot(int productId)
        {
            try
            {
            var product = await context.Products.FirstAsync(b => b.Id == productId);
                if (product != null)
                {
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<ActionResult<Product>> BuyLot(string namelot)
        {
            var product = await context.Products.FirstAsync(b => b.Name == namelot);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return product;
        }

        private static string CheckPhoto(string url)
        {
            if(url == null)
            {
                return "https://www.generationsforpeace.org/wp-content/uploads/2018/03/empty.jpg";
            }
            else
            {
                return url;
            }
        }

        public class ProductValidator : AbstractValidator<Product>
        {
           public ProductValidator()
            {
                RuleFor(product => product.Id).NotEmpty()
                   .WithMessage("Please provide correctly id");
                RuleFor(product  => product.Name).NotEmpty()
                    .WithMessage("Please enter correctly name");
                RuleFor(product => product.Price).NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("Please enter correctly price");
            }
        }


    }
}
