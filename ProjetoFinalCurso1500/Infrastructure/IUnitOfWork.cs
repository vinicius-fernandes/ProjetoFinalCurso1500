namespace ProjetoFinalCurso1500.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
