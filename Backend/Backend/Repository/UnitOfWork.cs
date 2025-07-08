using System.Runtime.Intrinsics.Arm;
using Backend.Models;
using Backend.Repository.Student;
using Microsoft.EntityFrameworkCore;
using WebAPI_ITI_DB.Models;


namespace Warehouse_Management_System.Repository
{
    public class UnitOfWork
    {
        private readonly dbContext db;
        private IStudentRepository _studentRepository;

        public UnitOfWork(dbContext db)
        {
            this.db = db;
        }

        public IStudentRepository studentRepository
        {
            get 
            {
                if(_studentRepository==null)
                    _studentRepository= new StudentRepository(db);
                return _studentRepository; 
            }
        }

        //public void save()
        //{
        //    dp.Save();
        //}
    }
}
