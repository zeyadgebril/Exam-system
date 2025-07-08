namespace Warehouse_Management_System.Repository
{
    public interface IRepository<T>where T : class
    {
        public List<T> getAll();
        public T getById(int id);
        public void Update(T entity);
        public void add(T entity);
        public void delete(T entity);
    }
}
