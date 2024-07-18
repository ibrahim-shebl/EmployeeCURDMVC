using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCURD.Models
{
        [Table("Departments", Schema = "HR")]
        public class Department
        {
            [Key]
            [Display(Name = "ID")]
            public int DepartmentId { get; set; }

            [Required]
            [Display(Name = "Department Name")]
            [Column(TypeName = "varchar(200)")]
            public string DepartmentName { get; set; } = string.Empty;
        }
}
