using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using PermisosEntitiesLib;
using Microsoft.AspNetCore.Mvc;
using System;


namespace PermisosWeb.Pages
{
    public class PermisosModel : PageModel 
    {
        [BindProperty]
        public Empleado Empleado {get; set;}
        public IEnumerable<string> Nombre { get; set; }
        public IEnumerable<string> apellidoPaterno { get; set; }
        public IEnumerable<string> apellidoMaterno { get; set; }
        public string nombreCompleto { get; set; }
        private Permisos db;

        public void OnGet(){
            Nombre = db.Empleados.Where(pass => pass.NumeroDeNomina == IndexModel.Nomina).Select(s => s.Nombres);
            apellidoPaterno = db.Empleados.Where(pass => pass.NumeroDeNomina == IndexModel.Nomina).Select(s => s.ApellidoPaterno);
            apellidoMaterno = db.Empleados.Where(pass => pass.NumeroDeNomina == IndexModel.Nomina).Select(s => s.ApellidoMaterno);
            nombreCompleto = Nombre.First() + " " + apellidoPaterno.First() + " " + apellidoPaterno.First();
            //+ apellidoPaterno.First + apellidoMaterno.First;
        }

        public PermisosModel(Permisos injectedContext)
        {
            db = injectedContext;
        }

        // public IActionResult OnPost()
        // {
           

        //     isInDB = db.Logins.Where(user => user.Usuario == Login.Usuario).Where( pass => pass.Password == Login.Password);
        //     if (isInDB.Any())
        //     {
                    
        //         return RedirectToPage("/permisos");
        //     }
              
                
         
        //     return Page();
      
        // }
  


    }

}