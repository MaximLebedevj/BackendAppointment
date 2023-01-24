

namespace domain.Repository
{
     public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();  // get all objects
        T Get(int id);  // get one object by id
        bool Create(T item);  // create the object
        void Update(T item);  // update the object 
        bool Delete(int id);  // delete the object
        void Save();  // Save changes
    }
}
