using Shop.Data;
using Shop.Entities;

namespace Shop.Services
{
    public class BargainService
    {
        private readonly DataContext context;

        public BargainService(DataContext context)
        {
            this.context = context;
        } 
        
        public Bargain MakeBargain(int userId,int productId,int proposedPrice)
        {
            //Add Validation

            var bargain = new Bargain
            {
                UserId = userId,
                OfferId = productId,
                NewPrice = proposedPrice,
                Status = BargainStatus.Pending
            };


            return bargain;
        }


    }
}
