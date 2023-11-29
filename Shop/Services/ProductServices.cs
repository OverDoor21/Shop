using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Entities;

namespace Shop.Services
{
    public class ProductServices
    {
        private readonly DataContext context;

        public ProductServices(DataContext context)
        {
            this.context = context;
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

        public async Task<ActionResult<Product>> Deletelot(int productId)
        {
            var product = await context.Products.FirstAsync(b => b.Id == productId);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return product;
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
                RuleFor(product  => product.Name).NotEmpty()
                    .WithMessage("Please enter correctly name");
                RuleFor(product => product.Price).NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("Please enter correctly name");
            }
        }


    }
}
