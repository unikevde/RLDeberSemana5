using RLDeberSemana5.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLDeberSemana5
{
    public class PersonRepository
    {
        string dbPath; // ruta
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }
        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }
        public PersonRepository(string dbPath1)
        {
            dbPath = dbPath1;
        }
        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre requerido");
                Persona person = new() { Name = name };
                result = conn.Insert(person);
                StatusMessage = string.Format("{0} records added (Nombre: {1}", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR{1}", name, ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR", ex.Message);
            }
            return new List<Persona>();
        }
        public void DeletePerson(int personId)
        {
            int result = 0;
            try
            {
                Init();
                result = conn.Delete(personId);
                // int result = await conn.DeleteAsync<Persona>(personId);
                StatusMessage = string.Format("Filas eliminadas: {0}", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar la persona con ID {0}: {1}", personId, ex.Message);
            }
        }

        public void UpdatePerson(Persona updatedPerson)
        {
            int result = 0;
            try
            {
                Init();
                if (updatedPerson == null)
                    throw new Exception("Persona actualizada no puede ser nula");

                result = conn.Update(updatedPerson);
                StatusMessage = string.Format("Filas actualizadas: {0}", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona: {0}", ex.Message);
            }

        }
    }
}

