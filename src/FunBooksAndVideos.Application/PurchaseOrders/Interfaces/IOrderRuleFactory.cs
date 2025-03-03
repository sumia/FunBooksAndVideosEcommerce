namespace FunBooksAndVideos.Application.PurchaseOrders.Interfaces
{
    public interface IOrderRuleFactory
    {
        IEnumerable<IOrderRule> GetRules();
    }
}
