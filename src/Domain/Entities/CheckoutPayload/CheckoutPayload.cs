namespace Domain.Entities.CheckoutPayload
{
    public class AddressPayload
    {
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string locality { get; set; }
        public string city { get; set; }
        public string region_code { get; set; }
        public string country { get; set; }
        public string postal_code { get; set; }
    }

    public class AmountPayload
    {
        public int value { get; set; }
        public string currency { get; set; }
    }

    public class CardPayload
    {
        public string number { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string security_code { get; set; }
        public HolderPayload holder { get; set; }
        public bool store => true;
    }

    public class ChargePayload
    {
        public string reference_id { get; set; }
        public string description { get; set; }
        public AmountPayload amount { get; set; }
        public PaymentMethodPayload payment_method { get; set; }
    }

    public class CustomerPayload
    {
        public string name { get; set; }
        public string email { get; set; }
        public string tax_id { get; set; }
        public List<PhonePayload> phones { get; set; }
    }

    public class HolderPayload
    {
        public string name { get; set; }
    }

    public class ItemPayload
    {
        public string reference_id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int unit_amount { get; set; }
    }

    public class PaymentMethodPayload
    {
        public string type => "CREDIT_CARD";
        public int installments { get; set; }
        public bool capture => true;
        public CardPayload card { get; set; }
    }

    public class PhonePayload
    {
        public string country { get; set; }
        public string area { get; set; }
        public string number { get; set; }
        public string type { get; set; }
    }

    public class CheckoutPayload
    {
        public string reference_id => $"{customer.email}_{DateTime.UtcNow.ToString("dd-MM-yy")}";
        public CustomerPayload customer { get; set; }
        public List<ItemPayload> items { get; set; }
        public Shipping shipping { get; set; }
        public List<string> notification_urls { get; set; }
        public List<ChargePayload> charges { get; set; }
    }

    public class Shipping
    {
        public AddressPayload address { get; set; }
    }
}
