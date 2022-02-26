Application prints out the receipt details for shopping baskets.

• Please include instructions for how to compile and run your application.
	No additional steps are required to compile and run the application. Only start debugging (f5) or start without debugging (ctrl+f5).

• Unit tests must be included and those tests should cover key components of the application.
	Unit tests cover formatter, writer, parser and processor.

There was one problem in the development of the application:
	Output 2:
	1 imported bag of Vanilla-Hazelnut Coffee: 11.55
	1 Imported Vespa: 17,251.5
	Sales Taxes: 2,250.8
	Total: 17,263.05

	When I calculate imported Vespa tax and rounded it up to the nearest multiple $0.05 
	I got 2,250.2. 
	Formula: 
		Math.Ceiling(price / multiple) * multiple. 
	The task says 2,250.25.

