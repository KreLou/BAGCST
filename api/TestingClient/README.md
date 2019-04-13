# Testing Client
 
 ## Description
 This is the testing-client for the api. Here you can test the performance on the get-controller.
 
 ## Usage
 Currently is only Contacts-Performance-Testing available.
 Other controller will follow soon.
 
 ### Steps for VisualStudio on Windows
 1. Go the Soltions and select "multiple startup projects"
 2. Select to following order: 1. api "Start without  debugging", 2. TestingClient "start"
 3. Go to "api", right click, properties
 4. select "Debuggen" and set the APP-URL to `http://localhost:55510/` and uncheck "Start Browser"
 
 ## Development
 
`TestingClient.Testing.Configuration.TestConditions` 
 - Contains the Conditions for Testing, like WaitingTime (`TestingClient.Testing.Configuration.WaitingMethod.IWaitingMethod`)
 - Contains the amount of Iterations (int)
 
 `TestingClient.Testing.Configuratuion.WaitingMethod`
 - `IWaitingMethod` is the interface
 - Possible classes are RandomWaiting and StaticWaiting.
 
 'TestingClient.Testing.HttpRequest'
 - this is the responsible class for the HTTP-Get Reuest. 
 - expect the request-URL in the constructor.
 - on `Run` the async Task starts to request the url, stors the RequestResponseInformation and tracks the elapsedMilliseconds
 
 `TestingClient.Testing.Performance.Abstract`
 - Responsible Class for performance-testing.
 - Request the url in the constructor.
 - on `PerformTest` the Test will start, configured by TestConditions.
 - finally generats the TestReport on return them on `generateTestReport`
 
 `TestingClient.Testing.Performance.Contacts.ContactsPerformanceTesting`
 - override the `PerformanceTesting` and set url to 'localhost:55510'
