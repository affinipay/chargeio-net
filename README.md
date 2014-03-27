chargeIO.net
============

chargeIO.net is a .NET api for http://chargeio.com

For for a full API reference you can visit: http://chargeio.com/docs/merchant/index.html

Quick Start
-----------

Add a reference to chargeIO.net.dll (You can find a pre-compiled version in the build folder of this repository or install chargeIO.net via NuGet)

Next you will need to provide chargeIO.net with your api key and password. There are 3 ways to do this: Choose one.

a) Add an AppSetting with your api key to your config (this is the easiest way)

	<appSettings>
	...
		<add key="ChargeIOAuthUser" value="[your auth username here]" />
		<add key="ChargeIOAuthPassword" value="[your auth password here]" />
	...
	</appSettings>

b) In your application initialization, use the configuration object (this is a programmatic way, but you only have to do it once during startup)

	Configuration.SetAuthUser("[your auth username here]");
	Configuration.SetAuthPassword("[your auth password here]");

c) In any of the service constructors documented below, you can optionally pass the auth user and password (ideal when you are supporting multiple merchants). i.e...

	TransactionService ts = new TransactionService("[merchant auth username]","[merchant auth password]");

Example:
--------

	Configuration.SetApiUrl("https://api.chargeio.com/v1");
	Configuration.SetAuthUser("[Your auth username]");
	Configuration.SetAuthPassword("[your auth password]");
	
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
