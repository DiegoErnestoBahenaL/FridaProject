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

        public long tipoEmpleado {get; set;}
        public static long Nomina { get; set; }
        private Permisos db;

        public IndexModel(Permisos injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult OnPost()
        {
           

            isInDB = db.Logins.Where(user => user.Usuario == Login.Usuario).Where( pass => pass.Password == Login.Password);
            if (isInDB.Any())
            {  
                Nomina = Login.Usuario;

                var tipoEmpleadoQuery = 
                (
                    from t in db.Empleados 
                    where t.NumeroDeNomina == IndexModel.Nomina

                    select new
                    {

                        TipoEmpleado = t.TipoEmpleado
                    }


                ).ToList();

                tipoEmpleado = tipoEmpleadoQuery[0].TipoEmpleado;
                return RedirectToPage("/permisos");
            }
              
                
         
            return Page();
      
        }
  


    }

}