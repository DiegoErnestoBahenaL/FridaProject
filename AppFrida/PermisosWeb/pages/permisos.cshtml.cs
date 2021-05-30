using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using PermisosEntitiesLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace PermisosWeb.Pages
{
    public class PermisosModel : PageModel 
    {
        [BindProperty]
        public Empleado Empleado {get; set;}
        public string Nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string  apellidoMaterno { get; set; }

        public List<string> Permisos {get; set;}
        public string Area {get; set;}
        public string nombreCompleto { get; set; }
        public long Nomina { get; set; }
        public string Fecha { get; set; }
        private Permisos db;

        public void OnGet(){

            var queryEmpleado =
            (    
                from e in db.Empleados
                join a in db.AreaTrabajos
                on e.AreaTrabajo equals a.IdAreaEmpleado
                where e.NumeroDeNomina == IndexModel.Nomina
                select new 
                { 
                    Nombre = e.Nombres, 
                    apellidoPaterno = e.ApellidoPaterno, 
                    apellidoMaterno = e.ApellidoMaterno, 
                    Area = a.DescripcionArea 
                }
            ).ToList();
              

            var queryPermisos = 
            (
                from p in db.Permiso
                join tp in db.TipoPermisos
                on p.TipoPermiso equals tp.IdTipoPermiso
                join ep in db.EstadoPermisos
                on p.EstadoPermiso equals ep.IdEstadoPermiso
                where p.Empleado == IndexModel.Nomina
                select new
                {
                    Folio = p.Folio,
                    Tipo = tp.DescripcionPermiso,
                    FechaElab = p.FechaElaboracion,
                    FechaInicio = p.FechaJustificacionInicio,
                    FechaFin = p.FechaJustificacionFin,
                    HoraInicio = p.HoraInicio,
                    HoraFin = p.HoraFin,
                    Estado = ep.DescripcionEstado
                }
            ).ToList();

         
            Nombre = queryEmpleado[0].Nombre;
            apellidoPaterno = queryEmpleado[0].apellidoPaterno;
            apellidoMaterno = queryEmpleado[0].apellidoMaterno;
            Area = queryEmpleado[0].Area;
            nombreCompleto = Nombre + " " + apellidoPaterno + " " + apellidoMaterno;
            Nomina = IndexModel.Nomina;
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            

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