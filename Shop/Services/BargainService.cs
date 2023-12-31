﻿using Shop.Data;
using Shop.Entities;
using FluentValidation;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Shop.Services
{
    public class BargainService
    {
        private readonly DataContext context;
        //In Database status for it stored as int 
        //0 for Pending 1 for Accepted and 2 for declained

        public BargainService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<Bargain>> CreateBargain(int userId, int productId, int proposedPrice)
        {
            if (!CanUserMakeBargain(userId, productId))
            {
                throw new InvalidOperationException("You cannot do this you reach of your limit");
            }
            //Check UserIsClient

            var user = await context.Users.FirstAsync(u => u.Id == userId);
            if(user.IsSeller())
            {
                throw new InvalidOperationException("You cannot create a bargain because you are not a client.");
            }

            //Check half price
            var product = await context.Products.FirstAsync(b => b.Id == productId);
            int oldprice = product.Price;
            if(proposedPrice <= oldprice / 2)
            {
                return new Bargain()
                {
                    Status = BargainStatus.Declined
                };
            }

            var bargain = new Bargain
            {
                UserId = userId,
                OfferId = productId,
                NewPrice = proposedPrice,
                Status = BargainStatus.Pending
            };

            await context.AddAsync(bargain);
            await context.SaveChangesAsync();

            return bargain;
        }
       
        public async Task<ActionResult<Bargain>> AcceptBargain(int userId, int bargainid)
        {
            var user = await context.Users.FirstAsync(u => u.Id == userId);
            if (!user.IsSeller())
            {
                throw new InvalidOperationException("You cannot Accept or Declain a bargain because you are not a Seller.");
            }

            var bargain = await context.Bargains.FirstAsync(b => b.Id == bargainid);
            var offer = await context.Products.FirstAsync(o => o.Id == bargain.OfferId);

            offer.Price = bargain.NewPrice;
            bargain.Status = BargainStatus.Accepted;
            await context.SaveChangesAsync();
            return bargain;
        }

        public async Task<ActionResult<Bargain>> DeclainBargain(int userId, int bargainid)
        {
            var user = await context.Users.FirstAsync(u => u.Id == userId);
            if (!user.IsSeller())
            {
                throw new InvalidOperationException("You cannot Accept or Declain a bargain because you are not a Seller.");
            }
            var bargain = await context.Bargains.FirstAsync(b => b.Id == bargainid);
            bargain.Status = BargainStatus.Declined;
            await context.SaveChangesAsync();

            return bargain;
        }

        private bool CanUserMakeBargain(int userid, int productid)
        {
            var bargainCount = context.Bargains.Count(b => b.UserId == userid
            && b.OfferId == productid);
            return bargainCount < 3;
        }
        //Validation Class for BargainService 
        private class BargainValidator : AbstractValidator<Bargain>
        {
           public BargainValidator()
            {
                RuleFor(bargain => bargain.NewPrice)
                     .NotEmpty().WithMessage("Provide price");
                RuleFor(id => id.Id)
                    .NotNull().WithMessage("You need to be authorised to use this");
            }
        }

    }
}
