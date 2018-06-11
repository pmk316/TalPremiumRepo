
Introduction

------------

Web application to compute the Premium based on the following rules
  
* Input fields:

    - Name

    - Date of Birth

    - Gender

  

* Based on the input, use the following to calculate a result:

    - Premium = Age * GenderFactor * 100

    - GenderFactor is based on the Gender the user supplied. 1.2 for a Male and 1.1 for a Female

    - The person can only receive a Premium if they are between the age of 18 and 65



Front-End (TalPremium.SPA)

--------------------------

Angular 4.4.7



Packages/Tools Used -

* Bootstrap V3.3.7




Back-End (TalPremium.API)

-------------------------

ASP .NET Core WebAPI V2.0.0



Packages/Tools Used -

* dotnet watcher tool



Unit Testing (TalPremium.API.UnitTests)

---------------------------------------

NUnit



Packages -

* Moq



List out the areas of improvement and refinement if you had a full 2 days to build this application.
----------------------------------------------------------------------------------------------------

1) Implement Global error handling in server side (in ASP .NET Core webapi code)


2) Implement Global error handling in client side (in Angular code)


3) Front end Improvements
   
   * Add more styling to the front end
   
   * Add more pages to front end - about, contact etc...


4) Add authentication in case the usecase demands it