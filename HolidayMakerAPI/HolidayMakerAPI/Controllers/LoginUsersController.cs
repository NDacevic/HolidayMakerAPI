﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI;
using HolidayMakerAPI.Data;

namespace HolidayMakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUsersController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public LoginUsersController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/LoginUsers/
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            User user = await _context.User.Select(u=>u).Where(u=>u.Email==email).FirstOrDefaultAsync();
            
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
