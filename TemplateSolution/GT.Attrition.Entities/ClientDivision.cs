using System.ComponentModel.DataAnnotations;

namespace NA.Template.Entities
{
    public class ClientDivision
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
