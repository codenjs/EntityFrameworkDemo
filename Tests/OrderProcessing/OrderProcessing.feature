Feature: OrderProcessing

Scenario: Customer with no discount pays full price
	Given TestCustomer has no discount
	And pizza costs $10.00
	When TestCustomer orders one pizza
	Then the total cost for TestCustomer is $10.00

Scenario: Customer with discount pays less than full price
	Given TestCustomer has a 50% discount
	And pizza costs $10.00
	When TestCustomer orders one pizza
	Then the total cost for TestCustomer is $5.00

Scenario: Orders for multiple customers are processed in one batch
	Given Customer A has no discount
	And Customer B has no discount
	And Customer C has a 50% discount
	And pizza costs $10.00
	When a batch of orders is processed:
	| Customer   | Product | Qty |
	| Customer A | pizza   | 2   |
	| Customer B | pizza   | 1   |
	| Customer C | pizza   | 1   |
	Then the total cost for Customer A is $20.00
	And the total cost for Customer B is $10.00
	And the total cost for Customer C is $5.00
