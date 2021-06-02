using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using PermisosEntitiesLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;



namespace PermisosWeb.Pages
{
    public class PermisosModel : PageModel
    {
        // [BindProperty]
        // public Empleado Empleado {get; set;}
        [BindProperty]
        public Permiso Permiso { get; set; }
        public string Nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public List<PermisosHandler> listPermisos { get; set; }
        public string Area { get; set; }
        public string nombreCompleto { get; set; }
        public long Nomina { get; set; }
        public long tipoEmpleado { get; set; }
        public string Fecha { get; set; }
        public long Estado { get; set; }
        private Permisos db;

        public void OnGet()
        {

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

            List<PermisosHandler> listaPermisos = new List<PermisosHandler>();

            foreach (var item in queryPermisos)
            {
                var p = new PermisosHandler()
                {
                    Folio = item.Folio.ToString(),
                    Tipo = item.Tipo,
                    FechaElab = item.FechaElab,
                    FechaInicio = item.FechaInicio,
                    FechaFin = item.FechaFin,
                    HoraInicio = item.HoraInicio,
                    HoraFin = item.HoraFin,
                    Estado = item.Estado
                };
                listaPermisos.Add(p);
            }

            listPermisos = listaPermisos;
            Nombre = queryEmpleado[0].Nombre;
            apellidoPaterno = queryEmpleado[0].apellidoPaterno;
            apellidoMaterno = queryEmpleado[0].apellidoMaterno;
            Area = queryEmpleado[0].Area;
            nombreCompleto = Nombre + " " + apellidoPaterno + " " + apellidoMaterno;
            Nomina = IndexModel.Nomina;
            tipoEmpleado = IndexModel.tipoEmpleado;
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public PermisosModel(Permisos injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult OnPostEnviarPermiso()
        {
            if (Permiso.HoraInicio == "7:00")
            {
                Permiso.HoraFin = "9:00";
            }
            else if (Permiso.HoraInicio == "9:00")
            {
                Permiso.HoraFin = "15:00";
            }
            else
            {
                Permiso.HoraFin = null;
            }

            Permiso.FechaJustificacionInicio = DateTime.Parse(Permiso.FechaJustificacionInicio).ToString("dd/MM/yyyy");
            Permiso.FechaJustificacionFin = DateTime.Parse(Permiso.FechaJustificacionFin).ToString("dd/MM/yyyy");

            Permiso.FechaElaboracion = DateTime.Now.ToString("dd/MM/yyyy");
            Permiso.Empleado = IndexModel.Nomina;
            Permiso.EstadoPermiso = 3; //Solicitado
            db.Permiso.Add(Permiso);
            db.SaveChanges();
            return RedirectToPage("/permisos");
        }

        public IActionResult OnPostCancelarPermisoEmpleado(int folio)
        {
            
            IEnumerable<Permiso> permisoDelete = db.Permiso.Where(pd => pd.Folio == folio);
            db.RemoveRange(permisoDelete);
            int affectedRows = db.SaveChanges();

            return RedirectToPage("/permisos");
        }

    }

    public class PermisosHandler
    {

#nullable enable
        public string Folio { get; set; }

        public string Tipo { get; set; }
        public string FechaElab { get; set; }

        public string FechaInicio { get; set; }

        public string FechaFin { get; set; }

        public string? HoraInicio { get; set; }

        public string? HoraFin { get; set; }

        public string Estado { get; set; }

    }

}