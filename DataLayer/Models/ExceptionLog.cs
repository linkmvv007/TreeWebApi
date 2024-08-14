using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public record ExceptionLog
{
    [Key]
    public int Id { get; set; }

    public int EventId { get; set; } //?

    public string? RequestParameters { get; set; }
    public string? RequestBody { get; set; }

    public string? StackTrace { get; set; }

   // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } // auto generation date time 
}