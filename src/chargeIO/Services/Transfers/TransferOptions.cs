using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChargeIO
{
    public class TransferOptions
    {
        private BankOptions _bank;

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("amount")]
        public int? AmountInCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        [JsonProperty("bank")]
        BankOptions Bank { get { return _bank; } }

        [JsonIgnore]
        public string BankName
        {
            get { return _bank == null ? null : _bank.Name; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.Name = value;
            }
        }

        [JsonIgnore]
        public string BankAccountNumber
        {
            get { return _bank == null ? null : _bank.AccountNumber; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.AccountNumber = value;
            }
        }
        [JsonIgnore]
        public string BankRoutingNumber
        {
            get { return _bank == null ? null : _bank.RoutingNumber; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.RoutingNumber = value;
            }
        }

        [JsonIgnore]
        public string BankAccountType
        {
            get { return _bank == null ? null : _bank.AccountType; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.AccountType = value;
            }
        }

        [JsonIgnore]
        public string Name
        {
            get { return _bank == null ? null : _bank.Name; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.Name = value;
            }
        }

        [JsonIgnore]
        public string Address1
        {
            get { return _bank == null ? null : _bank.Address1; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.Address1 = value;
            }
        }
        [JsonIgnore]
        public string Address2
        {
            get { return _bank == null ? null : _bank.Address2; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.Address2 = value;
            }
        }
        [JsonIgnore]
        public string City
        {
            get { return _bank == null ? null : _bank.City; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.City = value;
            }
        }
        [JsonIgnore]
        public string State
        {
            get { return _bank == null ? null : _bank.State; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.State = value;
            }
        }
        [JsonIgnore]
        public string PostalCode
        {
            get { return _bank == null ? null : _bank.PostalCode; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.PostalCode = value;
            }
        }
        [JsonIgnore]
        public string Country
        {
            get { return _bank == null ? null : _bank.Country; }
            set
            {
                _bank = _bank == null ? new BankOptions() : _bank;
                _bank.Country = value;
            }
        }
    }
    
}
