﻿using AuthService.Models;
using AuthService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly UserDetailsRepository _userRepo;
        private IConfiguration _config;

        public AuthController(UserDetailsRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        [HttpPost("User")]
        public UserDetails GetUser(UserDetails valuser)
        {
            var user = _userRepo.GetUserDetails(valuser);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDetails login)
        {
            _log4net.Info("Authentication initiated");
            IActionResult response = Unauthorized();
            UserDetails user = GetUser(login);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var tokenString = GenerateJSONWebToken(login);
                response = Ok(new { token = tokenString });
                return response;
            }
        }

        private string GenerateJSONWebToken(UserDetails userInfo)
        {
            _log4net.Info("Token Generation initiated");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
