using aaguirreS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aaguirreS5
{
    public class PersonRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public void Init() 
        {
            if(conn is not null) 
            
                return;
                conn = new(dbPath);
                conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath1)
        {
            dbPath= dbPath1;
        }

        //Insertar
        public void AddNewPerson(string nombre) 
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre Requerido");
                Persona persona = new Persona() { Name = nombre };
                result = conn.Insert(persona);
                StatusMessage = string.Format("Filas agregadas", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al insertar", nombre, ex.Message);
            }
        }

        //Listar
        public List<Persona> GetAllPorle()
        {
            try
            {
                Init();
                    return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al mostrar los datos", ex.Message);
            }
            return new List<Persona>();
        }
        
        //Actualizar
        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();

                // Verificamos si el nombre es válido
                if (string.IsNullOrEmpty(newName))
                    throw new Exception("Nuevo nombre requerido");

                // Buscamos la persona por su ID
                Persona existingPerson = conn.Find<Persona>(id);

                // Verificamos si la persona existe
                if (existingPerson != null)
                {
                    // Actualizamos el nombre de la persona
                    existingPerson.Name = newName;

                    // Ejecutamos la actualización en la base de datos
                    int result = conn.Update(existingPerson);

                    // Actualizamos el mensaje de estado
                    StatusMessage = result > 0
                        ? $"Persona actualizada: {existingPerson.Name}"
                        : "No se realizaron cambios";

                }
                else
                {
                    StatusMessage = "Persona no encontrada";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al actualizar: {ex.Message}";
            }
        }

        //Eliminar
        public void DeletePerson(int id)
        {
            try
            {
                Init();

                // Busca la persona por su ID
                Persona personToDelete = conn.Find<Persona>(id);

                // Verifica si la persona existe
                if (personToDelete != null)
                {
                    // Elimina la persona de la base de datos
                    int result = conn.Delete(personToDelete);

                    // Actualiza el mensaje de estado
                    StatusMessage = result > 0
                        ? $"Persona eliminada: {personToDelete.Name}"
                        : "No se realizaron cambios";
                }
                else
                {
                    StatusMessage = "Persona no encontrada";
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: Propaga la excepción
                throw new Exception($"Error al eliminar: {ex.Message}");
            }
        }


    }
}
