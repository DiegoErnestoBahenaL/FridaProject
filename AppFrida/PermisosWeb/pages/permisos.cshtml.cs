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
        public long idArea {get; set;}
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
                    Area = a.DescripcionArea,
                    idArea = a.IdAreaEmpleado
                }
            ).ToList();
            tipoEmpleado = IndexModel.tipoEmpleado;
            Nombre = queryEmpleado[0].Nombre;
            apellidoPaterno = queryEmpleado[0].apellidoPaterno;
            apellidoMaterno = queryEmpleado[0].apellidoMaterno;
            Area = queryEmpleado[0].Area;
            nombreCompleto = Nombre + " " + apellidoPaterno + " " + apellidoMaterno;
            Nomina = IndexModel.Nomina;
            idArea = queryEmpleado[0].idArea;
           
            Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            switch (tipoEmpleado)
            {
                case 1:

                
                    var queryPermisosDir =
                    (
                        from p in db.Permiso
                        join tp in db.TipoPermisos
                        on p.TipoPermiso equals tp.IdTipoPermiso
                        join ep in db.EstadoPermisos
                        on p.EstadoPermiso equals ep.IdEstadoPermiso
                        join emp in db.Empleados
                        on p.Empleado equals emp.NumeroDeNomina
                        where (p.EstadoPermiso == 4 || (emp.NumeroDeNomina == IndexModel.Nomina) ) 
                         
                        select new
                        {
                            Folio = p.Folio,
                            Tipo = tp.DescripcionPermiso,
                            FechaElab = p.FechaElaboracion,
                            FechaInicio = p.FechaJustificacionInicio,
                            FechaFin = p.FechaJustificacionFin,
                            HoraInicio = p.HoraInicio,
                            HoraFin = p.HoraFin,
                            Estado = ep.DescripcionEstado,
                            Nomina = emp.NumeroDeNomina
                        }
                    ).ToList();

                    List<PermisosHandler> listaPermisosDir = new List<PermisosHandler>();

                    foreach (var item in queryPermisosDir)
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
                            Estado = item.Estado,
                            Nomina = item.Nomina.ToString()
                        };
                        listaPermisosDir.Add(p);
                    }

                    listPermisos = listaPermisosDir;


                break;
                case 2:
                case 3:

 
                     var queryPermisosSup =
                    (
                        from p in db.Permiso
                        join tp in db.TipoPermisos
                        on p.TipoPermiso equals tp.IdTipoPermiso
                        join ep in db.EstadoPermisos
                        on p.EstadoPermiso equals ep.IdEstadoPermiso
                        join emp in db.Empleados
                        on p.Empleado equals emp.NumeroDeNomina
                        where ((p.EstadoPermiso == 3 && emp.AreaTrabajo == idArea)  || (emp.NumeroDeNomina == IndexModel.Nomina )) 
                         
                        select new
                        {
                            Folio = p.Folio,
                            Tipo = tp.DescripcionPermiso,
                            FechaElab = p.FechaElaboracion,
                            FechaInicio = p.FechaJustificacionInicio,
                            FechaFin = p.FechaJustificacionFin,
                            HoraInicio = p.HoraInicio,
                            HoraFin = p.HoraFin,
                            Estado = ep.DescripcionEstado,
                            Nomina = emp.NumeroDeNomina
                        }
                    ).ToList();

                    List<PermisosHandler> listaPermisosSup = new List<PermisosHandler>();

                    foreach (var item in queryPermisosSup)
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
                            Estado = item.Estado,
                            Nomina = item.Nomina.ToString()
                        };
                        listaPermisosSup.Add(p);
                    }

                    listPermisos = listaPermisosSup;



                break;
                case 4:
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
                break;
            }
   
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
            else if (Permiso.HoraInicio == "13:00")
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
            if (IndexModel.tipoEmpleado == 1)
            {
                Permiso.EstadoPermiso = 4; 
            }
            else
            {
                Permiso.EstadoPermiso = 3; //Solicitado
            }
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

        public IActionResult OnPostAceptarPermiso (int folio)
        {
            if (IndexModel.tipoEmpleado == 1)
            {
            Permiso aceptarPermiso = db.Permiso.First(pd => pd.Folio == folio);
            aceptarPermiso.EstadoPermiso = 1;
            int affected = db.SaveChanges();
            return RedirectToPage("/permisos");
            }
            else
            {
                Permiso aceptarPermiso = db.Permiso.First(pd => pd.Folio == folio);
                aceptarPermiso.EstadoPermiso = 4;
                int affected = db.SaveChanges();
                return RedirectToPage("/permisos");
            }
        }

        public IActionResult OnPostCancelarPermiso (int folio)
        {
                

            Permiso aceptarPermiso = db.Permiso.First(pd => pd.Folio == folio);
            if(aceptarPermiso.Empleado == IndexModel.Nomina)
            {
                    
                IEnumerable<Permiso> permisoDelete = db.Permiso.Where(pd => pd.Folio == folio);
                db.RemoveRange(permisoDelete);
                int affectedRows = db.SaveChanges();
                return RedirectToPage("/permisos");
            }
            aceptarPermiso.EstadoPermiso = 2;
            int affected = db.SaveChanges();
            
            return RedirectToPage("/permisos");
        }

        public IActionResult OnPostSalir ()
        {
             return RedirectToPage("/index");

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

        public string? Nomina {get; set;}

    }

}