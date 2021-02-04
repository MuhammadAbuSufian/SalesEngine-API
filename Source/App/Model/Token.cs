using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class  Token: EntityBase
    {
       public DateTime ExpireAt { get; set; }
        
       public string Ticket { get; set; }

       public string UserId { get; set; }

       [ForeignKey("UserId")]
       public User User { get; set; }
    }
}