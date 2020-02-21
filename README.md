# Paymenet Gateway API
This is an exercise for the coding chagelling from Checkout.com

<a href="https://github.com/rafaelqueiroz89/payment-gateway/actions?query=workflow%3A%22.NET+Core+build+script%22+branch%3Amaster">![.NET Core build script](https://github.com/rafaelqueiroz89/payment-gateway/workflows/.NET%20Core%20build%20script/badge.svg)</a> status from the <i>master</i> branch

Ideally we should not expose Domain objects to the external world, instead we should create an Adapter or use a framework like AutoMapper to transform my Domain object into a DTO and transfer it to different systems


# Business Discussion

We will be taking in account that the business needs 2 different flows, the first is requesting a payment from the merchant and the second one is retrieving a made transaction request. 

## Diagrams

<b>Merchant requests a payment</b>

![](docs/sequence1.jpg)
 
<b>Merchant requests to see the details of a Payment</b>

![](docs/sequence2.jpg)

<b>Overview of the big picture</b>

The actors of the whole system are:

![](docs/big_picture.jpg)

A. Shopper: Individual who is buying the product online. 
B. Merchant: The seller of the product.
C. Payment Gateway: Responsible for validating requests, storing card information and forwarding payment requests and accepting payment responses to and from the acquiring bank. 
D. Acquiring Bank: Allows us to do the actual retrieval of money from the shopper’s card and payout to the merchant. It also performs some validation of the card information and then sends the payment details to the appropriate 3rd party organization for processing.



# Technical Discussion

## Git branch strategy

Master branch rules:
 - It is protected against push
 - It doesn't allow a force push if the development branch is with build fail
 - Only accepts pull requests

## Techonologies, tools, methodologies and frameworks used

.NET Core 3.0
xUnit for unit testing
Some concepts of DDD
Serilog for application logging
Grafana for showing a dashboard of metrics
Prometheus for the metrics server
Docker to use containers (with docker-compose)
Github Actions for the Build script (CI)
Mrmaid-js for drawing the diagrams
Git 
Visual Studio 2019 Enterprise

Swagger.io pkg to simplify the API development
Mediatr pkg (implementation of the Mediator pattern) to handle the CQRS requests
Fluent Validator pkg to simplify the input validations with rules
CreditCardValidator nuget pkg to simplify the validation of a valid card
Microsoft.EntityFrameworkCore.InMemory nuget pkg to add the database in memory connection for testing

## System overview

<b>Request states lifecycle</b>

![](docs/state_diagram.jpg)

<b>Main domain classes relationship</b>

![](docs/aggregate_domain_class_diagram.jpg)
