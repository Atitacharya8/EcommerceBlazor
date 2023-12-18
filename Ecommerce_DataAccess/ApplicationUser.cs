using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_DataAccess
{
    public class ApplicationUser : IdentityUser // there will be two type of user (applicaitonuser and identityuser itself)
    {
        public string Name { get; set; } 
        
    }
}
