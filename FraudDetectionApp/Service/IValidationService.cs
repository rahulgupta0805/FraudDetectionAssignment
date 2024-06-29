using FraudDetection.Console.Model;

namespace FraudDetectionApp.Service
{
    public interface IValidationService
    {
        (bool, List<int>) ValidateUserOrders(List<OrderDetails> orderDetails);
    }
}
