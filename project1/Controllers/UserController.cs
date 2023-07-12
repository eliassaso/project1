using project1.Models;
using project1.Models.DAO;
using project1.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace project1.Controllers
{
    public class UserController : Controller
    {
        private UserDAO userRepository = new UserDAO();
        private AuthorizationConfig _db = new AuthorizationConfig();
        // GET: User
        public ActionResult Index()
        {
            
            List<UserDTO> users = userRepository.ReadUsers();
            // get an specific user (5)
            UserDTO user = (from u in users where 5 == u.Id select u).First();
            // get role
            RolesUsuario ru = _db.RolesUsuario.Where(x => x.IdUser == user.Id).FirstOrDefault();
            // asign a session variable  // role name
            Session["role"] = _db.Roles.Where(x => x.Id == ru.IdRole).FirstOrDefault().Description;

            return View(userRepository.ReadUsers());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDTO user)
        {
            if (ModelState.IsValid) {
                
                return RedirectToAction("Index");

            } else {
                userRepository.InsertUser(user);
            }

            return View();
        }

        [HttpGet]
        public ActionResult getRole(int id)
        {
            try { 
                int idRole = _db.RolesUsuario.Where(x => x.IdUser == id).FirstOrDefault().IdRole;
                return Content(_db.Roles.Where(x => x.Id == idRole).FirstOrDefault().Description);
            }
            catch (Exception ex) {
                return Content("DESCONOCIDO");
            }
        }
    }
}