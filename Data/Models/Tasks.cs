using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Web.Data.Models
{
    public class TasksToDo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
