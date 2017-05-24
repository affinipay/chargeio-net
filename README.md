# chargeio-net

[![standard-readme compliant](https://img.shields.io/badge/standard--readme-OK-green.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

> .NET client library for the AffiniPay payment gateway (aka ChargeIO)

## Overview
Use our official C# library to access the [AffiniPay payment gateway API](https://developers.affinipay.com/reference/api.html#PaymentGateway) from your .NET project.

## Security
This library requires a `secret_key` to authenticate with the AffiniPay payment gateway API. Don't inadvertently expose your `secret_key` in public web pages or source control repositories. Refer to the [Configuration](#configuration) section for more information.

## Installation
This library requires the [.NET Core SDK](https://www.microsoft.com/net/core). Make sure it's installed before proceeding.

Install the library from NuGet:

```
PM> Install-Package chargeio-net -Version 1.0.0
```

## Configuration
Access to the AffiniPay payment gateway requires a test or live-mode [`secret_key`](https://developers.affinipay.com/guides/payment-form-getting-started.html#obtain-credentials) for authentication and authorization. You must create an `appsettings.json` file that contains your `secret_key` and place it in the same directory as your project `.dll`.

Here's an example of an `appsettings.json` file:

```
{
    "ChargeIOSecretKey": "z6keqpRzRAiNNjvlJ3sL5wU46t1VGewnBu7Xzsed03W5b359KhDyapZWGnTiAFXM",
    "ChargeIOApiUrl": "https://api.chargeio.com",
    "ApiVersion": "/v1",
    "ChargeIOHTTPTimeout": "20000"
}
```

## Usage
The following example shows how to 1) exchange credit card details for a one-time token and 2) run a charge using that one-time token.

>We highly recommend masking payment details you collect using tokenization or saved cards/bank accounts.  Refer to [`PaymentMethodService`](#PaymentMethodService) for more information.

>If you choose not to use tokens or saved cards/banks and plan on handling sensitive cardholder data yourself, you’ll need to implement robust security policies and comply with [more stringent PCI requirements](https://developers.affinipay.com/basics/overview.html#pci-compliance).

```
try {
    // Invoke the PaymentMethodService to exchange payment details for a one-time token.
    PaymentMethodService pm = new PaymentMethodService();

    // Provide payment details as TokenOptions.
    Token token = pm.CreateToken(new TokenOptions(){
        Type = "card",
        CardNumber = "4242424242424242",
        CardExpirationMonth = 10,
        CardExpirationYear = 2020,
        CardCvv = "123",
        Name = "Dave Bowman",
        Address1 = "2001 Space Odyssey Ln",
        City = "Austin",
        State = "TX",
        PostalCode = "78750",
        Email = "devsupport@affinipay.com"
    });

    // Invoke the TransactionService to run a charge.
    TransactionService ts = new TransactionService();

    // Run a charge using the one-time token.
    Charge charge = ts.Charge(new ChargeOptions(){
        AmountInCents = 1000,
        Method = token.Id
    });
    await context.Response.WriteAsync(JObject.FromObject(charge).ToString());
    }

// Handle exceptions and print details to the screen.
catch(ChargeIOException e) {
    await context.Response.WriteAsync(e.Message);
}
```
## Documentation
The latest AffiniPay payment gateway API documentation is available at [https://developers.affinipay.com/reference/api.html#PaymentGatewayAPI](https://developers.affinipay.com/reference/api.html#PaymentGatewayAPI).

## Development
To successfully run tests, you must have an AffiniPay merchant account that matches the following configuration:

-   At least one test-mode ACH _and_ one test-mode credit account
-   No daily/monthly transaction limit set on your test-mode accounts
-   CVV policy set to "_Optional_"
-   AVS policy set to "_Address or Postal Code Match - Lenient_"
-   No additional **Required Payment Fields** checked other than those set by default after selecting a CVV/AVS policy

Contact [support](mailto:devsupport@affinipay.com) if you need an AffiniPay merchant account or to remove transaction limits from your test account(s). Refer to [AVS and CVV Policies](https://developers.affinipay.com/basics/account-management.html) for policy configuration information.

## API
This library contains the following services:

-   `MerchantService`
-   `PaymentMethodService`
-   `RecurringChargeService`
-   `TransactionService`

### MerchantService
Use this service to manage merchant account information, such as retrieving and updating business information and updating the details, policies, and status of [`merchant_accounts`](https://developers.affinipay.com/reference/api.html#merchant_account) and [`ach_accounts`](https://developers.affinipay.com/reference/api.html#ach_account).

This service includes the following methods:

-   `GetMerchant`
-   `UpdateMerchant`
-   `UpdateMerchantAccount`
-   `UpdateACHAccount`

Refer to [Merchant Management](https://developers.affinipay.com/reference/api.html#MerchantManagement) for more information.

### PaymentMethodService
To avoid storing sensitive card/bank details on your system, exchange payment details for one of the following payment methods:

-   **Saved card/bank** - Designed to support "remembered" payments for customers with no limits on future use.
-   **One-time token** - Designed for single use. Tokens expire after five minutes and are then deleted by the gateway.

Payment methods obscure sensitive aspects of payment details while providing an ID that you can use in any API that accepts [`card`](https://developers.affinipay.com/reference/api.html#card) or [`bank`](https://developers.affinipay.com/reference/api.html#bank) details in lieu of the card or bank JSON entity.

This service includes the following methods:

-   `CreateCard`
-   `DeleteCard`
-   `ListCards`
-   `CreateBank`
-   `DeleteBank`
-   `ListBanks`
-   `GetToken`
-   `CreateToken`

Refer to [Payment Methods](https://developers.affinipay.com/reference/api.html#PaymentMethods) for more information.

### RecurringChargeService
The AffiniPay payment gateway's recurring charge support makes it easy to collect payments from customers on an ongoing, scheduled basis--from simple donations to more complicated installment plans.

This service includes the following methods:

-   `RecurringCharge`
-   `RecurringCharges`
-   `GetRecurringCharge`
-   `UpdateRecurringCharge`
-   `CancelRecurringCharge`
-   `DeleteRecurringCharge`
-   `GetOccurrence`
-   `PayOccurrence`
-   `IgnoreOccurrence`
-   `Occurrences`

Refer to [Recurring Charges](https://developers.affinipay.com/reference/api.html#RecurringCharges) for more information.

### TransactionService
Simplifies the process of submitting payments and applying refunds to those payments. The gateway automatically performs funds capture daily and initiates settlement. Refunds can be performed at any time after a charge, in any number, up to the amount of the charge.

The API also includes methods for retrieving individual transactions by ID, as well as a search operation that returns paginated results based on criteria specified by the caller.

This service includes the following methods:

-   `Charge`
-   `Authorize`
-   `Void`
-   `Capture`
-   `Refund`
-   `Credit`
-   `GetTransaction`
-   `Transactions`
-   `Holds`
-   `Sign`
-   `GetSignature`

Refer to [Transactions](https://developers.affinipay.com/reference/api.html#Transactions) for more information.

## Contribute

Contributions in the form of GitHub pull requests are welcome. Please adhere to the following guidelines:
-   Before embarking on a significant change, please create an issue to discuss the proposed change and ensure that it is likely to be merged.
-   Follow the coding conventions used throughout the project, including 2-space indentation.
-   All contributions must be licensed under the MIT license.


## License

[MIT](./LICENSE) © AffiniPay LLC
