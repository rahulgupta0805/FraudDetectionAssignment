using FraudDetection.Console.Model;

namespace FraudDetectionApp.Service
{
    public class ValidationService : IValidationService
    {
        public (bool, List<int>) ValidateUserOrders(List<OrderDetails> orderDetails)
        {
            var ordersWithSameDealId = orderDetails.GroupBy(orderDetails => orderDetails.DealId).ToList();

            var fraudOrderIdList = new List<int>();

            foreach (var order in ordersWithSameDealId) {
                if (order.Count() > 1)
                {
                    var ordersWithDifferentHashCode = order.GroupBy(h => h.ContentHash)
                        .ToDictionary(x => x.Key, x => x.Select(e => e.OrderId))
                        .ToList();
                    if (ordersWithDifferentHashCode.Count() > 1) {

                        foreach (var item in ordersWithDifferentHashCode) 
                        {
                            fraudOrderIdList.Add(item.Value.First());
                        }
                        
                        return (false, fraudOrderIdList);
                    }
                }
            }

            return (true, fraudOrderIdList);
        }
    }
}
