using Microsoft.EntityFrameworkCore;
using rDetalle.DAL;
using rDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace rDetalle.BLL
{
    public class PersonasBLL
    {
        public static bool Guardar(Personas Persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Personas.Add(Persona) != null)
                    paso = db.SaveChanges() > 0;
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Modificar(Personas Persona)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var Anterior = db.Personas.Find(Persona.PersonaId);
                foreach (var item in Anterior.Telefonos)
                {
                    {
                        if (!Persona.Telefonos.Exists(d => d.Id == item.Id))
                            db.Entry(item).State = EntityState.Deleted;
                    }
                }

                // recorrer el detalle
                foreach(var item in Persona.Telefonos)
                {
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = estado;
                }
                // indicar que se esta modificando el encabezado.
                db.Entry(Persona).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var eliminar = db.Personas.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;

                paso = (db.SaveChanges() > 0);
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static Personas Buscar(int id)
        {
            Contexto db = new Contexto();
            Personas personas = new Personas();
            try
            {
                personas = db.Personas.Where(o => o.PersonaId == id)
                     .Include(o => o.Telefonos).SingleOrDefault();
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return personas;
        }
        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona)
        {
            List<Personas> Lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Personas.Where(persona).ToList();
            }catch(Exception)
            {
                throw;
            }finally
            {
                db.Dispose();
            }
            return Lista;

        }
    }

}
