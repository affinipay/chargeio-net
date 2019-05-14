# chargeio-net

[![standard-readme compliant](https://img.shields.io/badge/standard--readme-OK-green.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

> .NET client library for the AffiniPay Payment Gateway (aka ChargeIO)

## Overview
Use our official C# library to access the [AffiniPay Payment Gateway API](https://developers.affinipay.com/reference/api.html#PaymentGateway) from your .NET project.

## Security
This library requires a `secret_key` to authenticate with the AffiniPay Payment Gateway API. Don't inadvertently expose your `secret_key` in public web pages or source control repositories. Refer to the [Configuration](#configuration) section for more information.

## Installation
This library requires the [.NET Core SDK](https://docs.microsoft.com/en-us/dotnet/core/). Make sure it's installed before proceeding.

Install the library from NuGet:

```
PM> Install-Package chargeio-net -Version 1.1.0
```

## Configuration
Access to the AffiniPay Payment Gateway requires a test or live-mode secret_key for authentication and authorization. You can supply your secret_key to this library by using either of these two methods:

1. Pass your secret_key directly to the service constructors in your code.
Here's an example of creating a payment method service object by supplying a secret_key:

```PaymentMethodService pm = new PaymentMethodService("your-secret-key");```

2. Create an appsettings.json file that contains your secret_key and place it in the same directory as your project .dll. This can be accomplished in Visual Studio by adding your appsettings.json file to your project and setting the Copy to output directory property of the file to Always copy. This method would work if only a single client secret is needed in your application.

Here's an example of an appsettings.json file:

```
{
    "ChargeIOSecretKey": "z6keqpRzRAiNNjvlJ3sL5wU46t1VGewnBu7Xzsed03W5b359KhDyapZWGnTiAFXM",
    "ChargeIOApiUrl": "https://api.chargeio.com/v1",
    "ChargeIOHTTPTimeout": "20000"
}
```

## Usage

You must tokenize all sensitive payment information before you submit it to AffiniPay. On your
payment form, use AffiniPay’s hosted fields to secure payment data and call
window.AffiniPay.HostedFields.getPaymentToken to create a one-time payment token. See
["Creating payment forms using hosted fields"](https://developers.affinipay.com/collect/create-payment-form-hosted-fields.html). POST the token ID received to your C# application, and then perform the charge:


```
try {
    // Invoke the TransactionService to run a charge.
    TransactionService ts = new TransactionService();

    // Run a charge using the one-time token.
    Charge charge = ts.Charge(new ChargeOptions(){
        AmountInCents = 1000,
        Method = new TokenReferenceOptions() {
          TokenId = token.Id
        }
    });
    await context.Response.WriteAsync(JObject.FromObject(charge).ToString());
    }

// Handle exceptions and print details to the screen.
catch(ChargeIoException e) {
    await context.Response.WriteAsync(e.Message);
}
```
## Documentation
The latest AffiniPay Payment Gateway API documentation is available at [https://developers.affinipay.com/reference/api.html#PaymentGatewayAPI](https://developers.affinipay.com/reference/api.html#PaymentGatewayAPI).

## Development
To successfully run tests, you must have an AffiniPay merchant account that matches the following configuration:

-   At least one test-mode eCheck account (for eCheck payments)
-   At least one test-mode credit account (for credit card payments)
-   No daily/monthly transaction limit set on your test-mode accounts
-   CVV policy set to "_Optional_"
-   AVS policy set to "_Address or Postal Code Match - Lenient_"
-   No additional **Required Payment Fields** checked other than those set by default after selecting a CVV/AVS policy

Contact [support](mailto:devsupport@affinipay.com) if you need an AffiniPay merchant account or to remove transaction limits from your test account(s).

## API
This library contains the following services:

-   `MerchantService`
-   `PaymentMethodService`
-   `RecurringChargeService`
-   `TransactionService`

### MerchantService
Use this service to manage merchant account information, such as retrieving and updating  [`merchant`](https://developers.affinipay.com/reference/api.html#merchant) and [`ach_account`](https://developers.affinipay.com/reference/api.html#ach_account) information.

This service includes the following methods:

-   `GetMerchant`
-   `UpdateMerchant`
-   `UpdateACHAccount`

Refer to [Merchant Management](https://developers.affinipay.com/reference/api.html#MerchantManagement) for more information.

### PaymentMethodService
You can exchange a payment token for a saved card/bank, which is designed to support "remembered" payments for customers with no limits on future use. You can use these saved payment methods with any endpoint that accepts [`card`](https://developers.affinipay.com/reference/api.html#card) or [`bank`](https://developers.affinipay.com/reference/api.html#bank) details in lieu of the card or bank JSON entity.

This service includes the following methods:

-   `CreateCard`
-   `DeleteCard`
-   `ListCards`
-   `CreateBank`
-   `DeleteBank`
-   `ListBanks`
-   `GetToken`

Refer to [Payment Methods](https://developers.affinipay.com/reference/api.html#PaymentMethods) for more information.

### RecurringChargeService
The AffiniPay Payment Gateway's recurring charge support makes it easy to collect payments from customers on an ongoing, scheduled basis--from simple donations to more complicated installment plans.

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

The API also includes methods for retrieving individual transactions by ID or source ID, as well as a search operation that returns paginated results based on criteria specified by the caller.

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

### Handling errors from the Gateway
To handle and view errors emitted from the library catch on "ChargeIoException". ChargeIoException contains three parameters: an HTTP status code, a List of type "ChargeIoError", and a messages string.
ChargeIoErrors will give you the bulk of debug information. A "ChargeIoError" contains five parameters: a message, code, level, context, and sub_code.

// Handle exceptions and print details to the screen.
catch(ChargeIoException e) {
    await context.Response.WriteAsync(e.Message);
}

## Contribute

Contributions in the form of GitHub pull requests are welcome. Please adhere to the following guidelines:
-   Before embarking on a significant change, please create an issue to discuss the proposed change and ensure that it is likely to be merged.
-   Follow the coding conventions used throughout the project, including 2-space indentation.
-   All contributions must be licensed under the MIT license.


## License

[ISC](./LICENSE) © AffiniPay LLC
