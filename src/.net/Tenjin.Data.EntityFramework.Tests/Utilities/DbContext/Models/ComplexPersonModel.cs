using System.ComponentModel.DataAnnotations;

namespace Tenjin.Data.EntityFramework.Tests.Utilities.DbContext.Models;

public class ComplexPersonModel
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
}