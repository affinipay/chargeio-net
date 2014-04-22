chargeIO.net
============

.NET client library for the chargeIO payment gateway

Installation
-----------

To use the library, add a reference to chargeIO.net.dll (you can find a pre-compiled version in the build folder of this repository).

Access to the ChargeIO Gateway requires your test or live-mode secret key for authentication and authorization. You can provide your
secret key to the library in any of three ways to suit your development needs: 

a) Add an AppSetting with your secret key to your config (easiest)

	<appSettings>
	...
		<add key="ChargeIOSecretKey" value="[your secret key]" />
	...
	</appSettings>

b) In your application initialization, use the Configuration object (this is a programmatic mechanism, but is only performed once during startup)

	Configuration.SetSecretKey("[your secret key]");

c) In any of the service constructors (e.g., TransactionService), you can optionally pass the secret key (intended for scenarios in which you are supporting multiple merchants)

	TransactionService ts = new TransactionService("[merchant secret key]");

Example:
--------

	Configuration.SetApiUrl("https://api.chargeio.com/v1");
	Configuration.SetSecretKey("[your secret key]");
	
	try {
		
		TransactionService ts = new TransactionService();

		Charge charge = ts.Charge(new ChargeOptions(){
			AmountInCents = 100,
			Method = new CardOptions(){
				Name = "John Doe",
				Number = "378282246310005",
				ExpMonth = 12,
				ExpYear = 2016,
				Cvv = "123",
			}
		});

		Refund r = ts.Refund(charge.Id, 50, "Partial refund for merchandise return");
	}
	catch(ChargeIOException e) {
		System.Console.WriteLine(e.Message);
    }

Documentation
-----------

The latest ChargeIO API documentation is available at https://chargeio.com/developers.
