using System.Linq.Expressions;

namespace ProjetoFinalCurso1500.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById<C>(C id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        bool Exists<B>(B id);
    }
}
