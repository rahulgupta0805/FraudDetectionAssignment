// See https://aka.ms/new-console-template for more information
using FraudDetection.Console.Model;
using FraudDetectionApp.Service;

Console.WriteLine("Validate user order.");

IValidationService validationService = new ValidationService();

var orderDetails = new List<OrderDetails>
{
    new(1, 1, "bugs@bunny.com", "123 Sesame St.", "New York", "NY", 10011, 12345689010),
    new(2, 1, "elmer@fudd.com", "123 Sesame St.", "New York", "NY", 10011, 10987654321),
    new(3, 2, "bugs@bunny.com", "123 Sesame St.", "New York", "NY", 10011, 12345689010)
};

var (isValid, result) = validationService.ValidateUserOrders(orderDetails);

if (!isValid)
{
    foreach(var order in result)
    {
        Console.WriteLine($"Fraud Order detected with order id {order}");
    }
}
else
{
    Console.WriteLine("No fraud detected in user orders");
}

