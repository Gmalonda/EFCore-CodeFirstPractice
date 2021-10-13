# Diiage 2 : EF Core Code First Practice

> WARNING : in all step of this pratice, it's forbidden to use annotation in entities class, you have only to modify 
OrderDbContext class

## prerequisites
- Create a github account
- Visual studio 2019 or Sup
- .Net 5 SDK Installed on your laptop
- SQL Server 2017 or sup up and running on your laptop

## 1 : Fork Repository
- Fork this repository and create new branch for your code
- Change the fill SQL connection string in the class "ConfigurationConst" with your local SQL Server database ConnectionString
- Update Setup Method in the test class to apply database modification at each  

## 2 : Create your first migration
- Create a new migration which call 'InitialCreation'
- Run Unit Test
## 3 : Change name of a field in database
- With a configuration, you have to change the FirstName field in the Customer table to CustomerFirstName, 
- With a configuration, you have to change the table Name in of customer table to CustomerWithOrder, 
- Create a new migration which call 'ChangeTableAndFieldName'
- Run Unit Test
## 4 : Change max length of field 
- The field CustomerName in customer table must have a max length of 50 char
- Create a new migration which call 'ChangeMaxLength'
- Run Unit Test
## 5 : Change FK name 
- The customer FK in OrderTable must be : "FK_CustomerID"
- Create a new migration which call 'ChangeFKName'
- Run Unit Test

## 6: Post your results
- Add a screenshot of your test result in your ReadMe.md
- Comit and push your result in your git repository
- Send the adress of your repository at Gaultier Large and Michel Girard on Teams

## 7 : Result test
- https://github.com/Gmalonda/EFCore-CodeFirstPractice/blob/evalgedeon/CaptureResultTest.PNG
