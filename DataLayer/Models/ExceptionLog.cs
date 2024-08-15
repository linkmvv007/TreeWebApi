using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

[Index("CreatedAt", IsUnique = false)]
public record ExceptionLog
{
    [Key]
    public int Id { get; set; }

    public int EventId { get; set; } //?

    public string? RequestParameters { get; set; }
    public string? RequestBody { get; set; }

    public string? StackTrace { get; set; }

    public DateTime CreatedAt { get; set; } // auto generation date time 
}