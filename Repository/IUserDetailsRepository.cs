using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    public interface IUserDetailsRepository
    {
        UserDetails GetUserDetails(UserDetails user);
    }
}
