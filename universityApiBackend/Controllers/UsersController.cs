using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using universityApiBackend.DataAccess;
using universityApiBackend.Models.DataModels;
using universityApiBackend.Repositories;

namespace universityApiBackend.Controllers
{
    [Route("api/[controller]")] // controller for Request to localhost:7121/api/Users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: https://localhost:7121/api/Users
        [HttpGet]
        public IActionResult GetUsers() // va a ser de tipo taks en la cual el resultado de mi accion me va a retornar un ienumerable(osea un array) de tipo user
        {
            var users = _userRepository.GetAllUsers();
            var usersFiltrered = from user in users
                                 where user.IsDeleted == false
                                 select user;
            
            return Ok(usersFiltrered);
        }

        // GET: https://localhost:7121/api/Users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)// es una task que el resultado de accion va a returnar un usuario
        {
            var user = _userRepository.FindById(id); //busca FindAsync busca un objeto por la clave primaria de forma asyncrona en la base de datos

            if (user == null || !user.IsDeleted)
            {
                return NotFound();// me retorna un 404
            }

            return Ok(user);
        }

        // PUT: https://localhost:7121/api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user) // el iActionResult hace referencia a que devuelve solo un codigo http de respuesta
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            user.UpdatedAt = DateTime.UtcNow;
            

            try
            {
                _userRepository.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: https://localhost:7121/api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user) // a travez del body de nuestra petición voy a pedir un usuario
        {
            user.IsDeleted = false;
            //_context.Users.Add(user); //en el contexto de voy a decir a mi base de datos que me agregue el usuario que pédi por parametros
            
            if (user.rol == Rol.Admin)
            {
                user.rol = Rol.User;
            }else
            {
                user.rol = Rol.User;
            }
            //await _context.SaveChangesAsync(); // hacemos una async await o espera activa para que el contexto puedsa guardar los cambios de forma asyncrona
            _userRepository.Save(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user); //se va a retornar un Created at action (codigo http 201) en la cual se llama al metodo GetUser con el id de nuestro usuario creado 
        }

        // DELETE: https://localhost:7121/api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsDeleted=true;
            _userRepository.Update(user);

            return Ok();
        }

        private bool UserExists(int id)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            } //_context.Users.Any(e => e.Id == id);
        }
    }
}
