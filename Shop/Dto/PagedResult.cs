using Shop.Entities;

namespace Shop.Dto
{
    public class PagedResult<T>
    {
        public List<T> Products { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems    { get; set; }
      
        public PagedResult(List<T> product,int totalcount,int pageSize,int pagenumber) {
            Products = product;
            TotalItems = totalcount;
            TotalPages = (int)Math.Ceiling(totalcount / (double)pageSize);


        }

    }
}
