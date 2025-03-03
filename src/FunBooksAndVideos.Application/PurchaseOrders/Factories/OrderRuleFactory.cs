using FunBooksAndVideos.Application.PurchaseOrders.Interfaces;

namespace FunBooksAndVideos.Application.PurchaseOrders.Factories
{
    public class OrderRuleFactory : IOrderRuleFactory
    {
        private readonly IEnumerable<IOrderRule> _rules;


        public OrderRuleFactory(IEnumerable<IOrderRule> rules)
        {
            _rules = rules;
        }

        public IEnumerable<IOrderRule> GetRules()
        {
            return _rules;
        }
    }
}
