using Microsoft.AspNetCore.Authorization;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.Extensions.Configuration;  
using Microsoft.IdentityModel.Tokens;  
using System;  
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using System.Text;  
using TodoApi.Models;

namespace TodoApi.Controllers  
{  
    [Route("api/[controller]")]  
    [ApiController]  
    public class LoginController : Controller  
    {  
        private IConfiguration _config;  
        //Guid guid;
    
        public LoginController(IConfiguration config)  
        {  
            _config = config;  

           // guid = Guid.NewGuid();
        }  
        
        [HttpPost]  
        [AllowAnonymous]
        public IActionResult Login([FromBody]User login)  
        {  
            IActionResult response = Unauthorized();  
			//Método responsable de Validar las credenciales del User y devolver el modelo User
		    //Para demostración (en este punto) he usado datos de prueba sin persistencia de Datos
			//Si no retorna un objeto nulo, se procede a generar el JWT.
			//Usando el método GenerateJSONWebToken
            var user = AuthenticateUser(login);  
    
            if (user != null)  
            {  
                var tokenString = GenerateJSONWebToken(user);  
                response = Ok(new { token = tokenString });  
            }  
    
            return response;  
        }  
    
        private string GenerateJSONWebToken(User userInfo)  
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("CreatedAt", userInfo.CreatedAt.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
    
			//Se crea el token utilizando la clase JwtSecurityToken
			//Se le pasa algunos datos como el editor (issuer), audiencia
			// tiempo de expiración y la firma.
			
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],  
                _config["Jwt:Issuer"],  
                claims,  
                expires: DateTime.Now.AddMinutes(120),  
                signingCredentials: credentials
            );  
    
			//Finalmente el método JwtSecurityTokenHandler genera el JWT. 
			//Este método espera un objeto de la clase JwtSecurityToken 
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }  
    
        private User AuthenticateUser(User login)  
        {  
            User user = null;  
    
            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            if (login.UserName == "Daniel")  
            {  
                //user = new User { username = "Daniel", password = "123456" };  
                user = new User {UserName = login.UserName, Password=login.Password,Email=login.Email,CreatedAt=login.CreatedAt};
            }  
            return user;  
        }  
    }  
}  