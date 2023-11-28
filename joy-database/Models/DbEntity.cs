using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace joy_database.Models;

public class DbEntity
{
   [Key]
   public Guid Id { get; set; } 
   public DateTime Created { get; set; }
}