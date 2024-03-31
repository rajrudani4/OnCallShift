﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using ChatApp.Business.Helpers;
using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context.EntityClasses;
using ChatApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Private fields
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }
        #endregion

        #region API End points

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var usersList = _userService.GetAll();
                return Ok(usersList);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{username}")]
        public IActionResult UpdateProfile([FromForm] UpdateModel updateModel, string username)
        {
            try
            {
                string tokenUName = JwtHelper.GetUsernameFromRequest(Request);
                if (!tokenUName.Equals(username))
                {
                    return Unauthorized();
                }

                //get updated user
                var updated = _userService.UpdateUser(updateModel, username);

                if (updated.UserName == null || updated.UserName.Length == 0)
                {
                    return BadRequest(new { Message = "Email already exists. Please try again" });
                }

                if (updated != null)
                {
                    var tokenString = GenerateJSONWebToken(updated);
                    return Ok(new { token = tokenString, user = updated });
                }

                //error
                return BadRequest(new { Message = "Error occured while updating user profile. Please try again" });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            try
            {
                var user = _userService.GetUser(e => e.UserName == username, false);
                var userDto = ModelMapper.ConvertProfileToDTO(user);
                return Ok(userDto);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetUsers/{name}")]
        public IActionResult GetUsers(string name)
        {
            try
            {
                string username = JwtHelper.GetUsernameFromRequest(Request);
                var userList = _userService.GetAll(name.ToUpper().Trim(), username);
                return Ok(new { data = userList });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("change")]
        public IActionResult UpdateProfileStatus([FromQuery] string Status)
        {
            try
            {
                string userName = JwtHelper.GetUsernameFromRequest(Request);
                if (userName == null)
                {
                    return BadRequest();
                }

                var User = _userService.GetUser(e => e.UserName.Equals(userName));
                if (User == null)
                {
                    return BadRequest("User does not exists.");
                }

                var status = _userService.UpdateProfileStatus(Status, User);

                if (status.Length == 0)
                {
                    return BadRequest("Invalid Status");
                }

                return Ok(new { status });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region Methods
        private string GenerateJSONWebToken(Profile profileInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, profileInfo.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, profileInfo.Email),
                    new Claim(ClaimsConstant.FirstNameClaim, profileInfo.FirstName),
                    new Claim(ClaimsConstant.LastNameClaim, profileInfo.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddSeconds(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion


        //[HttpGet("GetImage")]
        //public async Task<IActionResult> GetImage([FromHeader] string authorization)
        //{
        //    //retrive token from request headers
        //    var token = JwtHelper.GetToken(authorization.Split()[1]);

        //    //get username from claims
        //    string username = token.Claims.First(c => c.Type == "sub").Value;
        //    //string path = token.Claims.First(c => c.Type == "imageUrl").Value;

        //    var user = _userService.GetUser(user => user.UserName == username, false);

        //    //if image is not set
        //    if (user.ImageUrl == null)
        //    {
        //        return NoContent();
        //    }

        //    var folderName = Path.Combine("Resources", "Images");
        //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //    string path = Path.Combine(pathToSave, user.ImageUrl);

        //    //if image does not exist
        //    if (!System.IO.File.Exists(path))
        //    {
        //        return NoContent();
        //    }

        //    Byte[] b;
        //    b = System.IO.File.ReadAllBytes(path);
        //    return File(b, "image/jpeg");
        //}
    }
}
