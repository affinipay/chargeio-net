using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChargeIO
{
    public class ChargeOptions
    {
        private CardOptions _card;

        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("auto_capture")]
        public bool AutoCapture { get; set; }
        
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("card")]
        CardOptions Card { get { return _card; } }

        [JsonIgnore]
        public string CardName
        {
            get { return _card == null ? null : _card.Name; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.Name = value;
            }
        }
        [JsonIgnore]
        public string CardNumber
        {
            get { return _card == null ? null : _card.Number; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.Number = value;
            }
        }
        [JsonIgnore]
        public string CardCvv
        {
            get { return _card == null ? null : _card.Cvv; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.Cvv = value;
            }
        }
        [JsonIgnore]
        public int? CardExpMonth
        {
            get { return _card == null ? null : _card.ExpMonth; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.ExpMonth = value;
            }
        }
        [JsonIgnore]
        public int? CardExpYear
        {
            get { return _card == null ? null : _card.ExpYear; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.ExpYear = value;
            }
        }
        [JsonIgnore]
        public string CardAddress1
        {
            get { return _card == null ? null : _card.Address1; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.Address1 = value;
            }
        }
        [JsonIgnore]
        public string CardAddress2
        {
            get { return _card == null ? null : _card.Address2; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.Address2 = value;
            }
        }
        [JsonIgnore]
        public string CardCity
        {
            get { return _card == null ? null : _card.City; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.City = value;
            }
        }
        [JsonIgnore]
        public string CardState
        {
            get { return _card == null ? null : _card.State; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.State = value;
            }
        }
        [JsonIgnore]
        public string CardPostalCode
        {
            get { return _card == null ? null : _card.PostalCode; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.PostalCode = value;
            }
        }
        [JsonIgnore]
        public string CardCountry
        {
            get { return _card == null ? null : _card.Country; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.Country = value;
            }
        }
        [JsonIgnore]
        public string CardType
        {
            get { return _card == null ? null : _card.Type; }
            set
            {
                _card = _card == null ? new CardOptions() : _card;
                _card.Type = value;
            }
        }

    }
}
