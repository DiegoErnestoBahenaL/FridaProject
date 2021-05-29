using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using PermisosEntitiesLib;
using Microsoft.AspNetCore.Mvc;
using System;


namespace PermisosWeb.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login Login {get; set;}
        public IEnumerable<bool> isInDB { get; set; }

        private Permisos db;

        public LoginModel(Permisos injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {

                isInDB = db.Logins.Select(user => user.Usuario == Login.Usuario);
                if (isInDB != null)
                {
                    
                    return RedirectToPage("/permisos.html");
                }
                Console.WriteLine(isInDB);
                
            }
            return Page();
      
        }
  


    }

}