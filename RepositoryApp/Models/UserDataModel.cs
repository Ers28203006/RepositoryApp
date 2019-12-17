namespace RepositoryApp.Models
{
    using System.Data.Entity;

    public partial class UserDataModel : DbContext
    {
        public UserDataModel()
            : base("name=UserDataModel")
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
