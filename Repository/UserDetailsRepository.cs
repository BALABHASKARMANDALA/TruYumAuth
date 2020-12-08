using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    public class UserDetailsRepository: IUserDetailsRepository
    {
        public static List<UserDetails> user = new List<UserDetails>()
        {
            new UserDetails{Userid=1,Username="abc",Password="abc@123"},
            new UserDetails{Userid=2,Username="xyz",Password="xyz@123"}
        };

        public UserDetails GetUserDetails(UserDetails valuser)
        {
            return user.FirstOrDefault(c => c.Username == valuser.Username && c.Password == valuser.Password);
        }
    }
}
