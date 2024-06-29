using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.Console.Model
{
    public record OrderDetails (
        int OrderId,
        int DealId,
        string EmailAddress,
        string StreetAddress,
        string City,
        string State,
        int ZipCode,
        long CreditCardNumber)
    {
        public string ContentHash => GetContentHash(StreetAddress.ToLower(), CreditCardNumber);

        private string GetContentHash(string streetAddress, long creditCardNumber)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedString = $"{streetAddress}{creditCardNumber}";
                var bytes = Encoding.UTF8.GetBytes(combinedString);
                var hashBytes = sha256.ComputeHash(bytes);
                var stringBuilder = new StringBuilder();
                foreach (var item in hashBytes) { 
                    stringBuilder.Append(item.ToString("X"));
                }
                return stringBuilder.ToString();
            } 
        }
    }
}
