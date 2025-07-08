using Backend.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI_ITI_DB.Models;

namespace Backend.Repository.Student
{
    public class StudentRepository : IStudentRepository
    {
        private readonly dbContext db;

        public StudentRepository(dbContext db)
        {
            this.db = db;
        }
        public void add(Students entity)
        {
            //dp.Students.Add(entity);
        }

        public void delete(Students entity)
        {
            //db.Set<Students>().Remove(entity);
        }

        public List<Students> getAll()
        {
            //return db.Set<Students>().ToList();
            return new List<Students>();
        }

        public Students getById(int id)
        {
            //return db.Set<Students>()
            //         .FirstOrDefault(e => e.StudentID == id);
            return new Students();
        }

        public void Update(Students entity)
        {
            //db.Entry(product).State =

            //db.ent (entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
