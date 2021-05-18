using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ServerApp.Entities
{
    public class User : IdentityUser<int>
    {
      public string Name { get; set; }
      public string Gender { get; set; }
      public DateTime DateOfBirth { get; set; }
      public DateTime Created { get; set; }
      public DateTime LastActive { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
      public string Introduction { get; set; }
      public string Hobbies { get; set; }
      public virtual List<Image> Images { get; set; }
    }
}