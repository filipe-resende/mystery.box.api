namespace Domain.Entities.CheckoutPayload
{
    public class CheckoutResponsePayload
    {
        public string id { get; set; }
        public string reference_id { get; set; }
        public DateTime created_at { get; set; }
        public CustomerResponsePayload customer { get; set; }
        public List<ItemResponsePayload> items { get; set; }
        public ShippingResponsePayload shipping { get; set; }
        public List<ChargeResponsePayload> charges { get; set; }
        public List<string> notification_urls { get; set; }
    }

    public class AddressResponsePayload
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

    public class AmountResponsePayload
    {
        public int value { get; set; }
        public string currency { get; set; }
    }

    public class CardResponsePayload
    {
        public string id { get; set; }
        public string brand { get; set; }
        public string first_digits { get; set; }
        public string last_digits { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public HolderResponsePayload holder { get; set; }
        public bool store { get; set; }
    }

    public class ChargeResponsePayload
    {
        public string id { get; set; }
        public string reference_id { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime paid_at { get; set; }
        public string description { get; set; }
        public AmountResponsePayload amount { get; set; }
        public PaymentResponsePayload payment_response { get; set; }
        public PaymentMethodResponsePayload payment_method { get; set; }
    }

    public class CustomerResponsePayload
    {
        public string name { get; set; }
        public string email { get; set; }
        public string tax_id { get; set; }
        public List<PhoneResponsePayload> phones { get; set; }
    }

    public class HolderResponsePayload
    {
        public string name { get; set; }
    }

    public class ItemResponsePayload
    {
        public string reference_id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int unit_amount { get; set; }
    }

    public class LinkPayload
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string media { get; set; }
        public string type { get; set; }
    }

    public class PaymentMethodResponsePayload
    {
        public string type { get; set; }
        public int installments { get; set; }
        public bool capture { get; set; }
        public CardResponsePayload card { get; set; }
        public string soft_descriptor { get; set; }
    }

    public class PaymentResponsePayload
    {
        public string code { get; set; }
        public string message { get; set; }
        public string reference { get; set; }
        public RawDataResponsePayload raw_data { get; set; }
    }

    public class PhoneResponsePayload
    {
        public string type { get; set; }
        public string country { get; set; }
        public string area { get; set; }
        public string number { get; set; }
    }

    public class RawDataResponsePayload
    {
        public string authorization_code { get; set; }
        public string nsu { get; set; }
        public string reason_code { get; set; }
    }

    public class ShippingResponsePayload
    {
        public AddressResponsePayload address { get; set; }
    }

    public class SummaryPayload
    {
        public int total { get; set; }
        public int paid { get; set; }
        public int refunded { get; set; }
    }
}