namespace Shop.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        
        public bool IsSeller()
        {
            return UserType == "Seller";
        }

    }
}
