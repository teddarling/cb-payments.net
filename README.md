cb-payments.net
===============

ClickBank is a great payment provider for people looking to get into marketing digital products. 
This is a C# library to access and use the various parts of the system that they provide access to.

#### Example: Get whatever ClickBank Sends
```C#
var auth = new Cb.Payments.Model.Authorization
    {
        DevKey = "ValidDevKey",
        ClerkKey = "ValidClerkKey"
    }


var service = new OrderService(auth, new HttpClient());

// Just take whatever ClickBank gives you
var orderResponse = service.GetList();

```

#### Example: Get Page 2 of results

If there is more than 100 records to return, ClickBank sends back a PartialContent result. This example would show
how to get the next page of results

```C#
var auth = new Cb.Payments.Model.Authorization
    {
        DevKey = "ValidDevKey",
        ClerkKey = "ValidClerkKey"
    }


var service = new OrderService(auth, new HttpClient());

// Just take whatever ClickBank gives you
var orderResponse = service.GetList();

OrderResponse nextResults;

if (orderResponse.Status == HttpStatusCode.PartialContent)
    nextResults = service.GetList(2);

```

#### Example: Get Orders by a specific Email

This example would get all orders with an email address of someemail@example.com

```C#
var auth = new Cb.Payments.Model.Authorization
    {
        DevKey = "ValidDevKey",
        ClerkKey = "ValidClerkKey"
    }


var service = new OrderService(auth, new HttpClient());

var queryParams = new Dictionary<string,string>();
queryParams.Add("email", "someemail@example.com");

// Just take whatever ClickBank gives you
var orderResponse = service.GetList(queryParams);

```

#### Example: Get Orders by a specific Email

This example would get all orders with an email address of someemail@example.com, retrieving the second page 
of results (yeah you hope the same email has had over 100 orders from you)

```C#
var auth = new Cb.Payments.Model.Authorization
    {
        DevKey = "ValidDevKey",
        ClerkKey = "ValidClerkKey"
    }


var service = new OrderService(auth, new HttpClient());

var queryParams = new Dictionary<string,string>();
queryParams.Add("email", "someemail@example.com");

// Just take whatever ClickBank gives you
var orderResponse = service.GetList(queryParams, 2);

```
