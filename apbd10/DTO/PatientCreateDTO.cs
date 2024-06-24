using System.ComponentModel.DataAnnotations;

namespace apbd10.DTO;

public class PatientCreateDTO
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }
}