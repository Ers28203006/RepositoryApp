using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryApp.Models
{
    [Table ("dbo.MOCK_DATA")]
    public class User
    {
        [Column("id")]
        [JsonProperty("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("first_name")]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [Column("last_name")]
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [Column("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [Column("gender")]
        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}