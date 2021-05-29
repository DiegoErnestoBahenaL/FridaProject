using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using PermisosEntitiesLib;
using Microsoft.AspNetCore.Mvc;
using System;


namespace PermisosWeb.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Login Login {get; set;}
        public IQueryable<Login> isInDB { get; set; }

        private Permisos db;

        public IndexModel(Permisos injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult OnPost()
        {
           

            isInDB = db.Logins.Where(user => user.Usuario == Login.Usuario);
            if (isInDB.Any())
            {
                    
                return RedirectToPage("/permisos");
            }
              
                
         
            return Page();
      
        }
  


    }

}