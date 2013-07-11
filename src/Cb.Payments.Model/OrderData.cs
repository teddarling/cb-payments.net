using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cb.Payments.Model
{
    public class OrderData
    {
        public IList<Order> Orders { get; set; }
    }

    public class Order
    {
        public DateTimeOffset Date { get; set; }
        public string Receipt { get; set; }
        public string Promo { get; set; }
        public string PmtType { get; set; }
        public string TxnType { get; set; }
        public string Item { get; set; }
        public string Amount { get; set; }
        public string Site { get; set; }
        public string Affi { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public string Zip { get; set; }
        public decimal? RebillAmount { get; set; }
        public int? ProcessedPayments { get; set; }
        public int? FuturePayments { get; set; }
        public DateTimeOffset? NextPaymentDate { get; set; }
        public string Status { get; set; }
        public decimal? AccountAmount { get; set; }
        public string Role { get; set; }
        public string CustomerDisplayName { get; set; }
        public string Title { get; set; }
        public bool Recurring { get; set; }
        public bool Physical { get; set; }
    }
}
