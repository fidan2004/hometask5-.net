using System.ComponentModel.DataAnnotations;

namespace Logging_HomeTask5.Dto
{
    public class EmployeeDto
    {
        [Required]
       
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        [Required]
        public int Salary { get; set; }
        public bool IsManager { get; set; }

    }
}
