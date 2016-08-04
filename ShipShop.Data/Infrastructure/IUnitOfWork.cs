namespace ShipShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}