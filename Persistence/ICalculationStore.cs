namespace thangi_calucator.Persistence
{
    public interface ICalculationStore
    {
        Task SaveAsync(Calculation calculation);
        Task<IReadOnlyList<Calculation>> LoadAllAsync();
    }
}