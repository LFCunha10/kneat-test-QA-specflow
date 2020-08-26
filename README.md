# Test for Quality Assurance Position

This is a .net core project, running on Specflow Framework.

## Pre-requirements

1. Webdriver

    This project can be executed against Chrome and Edge. Make sure that you have both installed.

1. Visual Studio 2019

1. Test Explorer on Visual Studio

    Can be found at `Test menu > Test Explorer`

1. SpecFlow Activation

    When you will run SpecFlow Framework for the first time, you can be asked to free activate it. 

    If when you run the tests, nothing happens:

    1. Go to `project folder > Test Results` and open the first text file. You will se the link for activation.


## How to read the tests

This project was written using BDD, with Gherkin language (Cucumber), and structured following the "Page Object Model" Design Pattern.

The framework SpecFlow will bind Each line of the test script (BDD) with a correspondent step definition. These step definitions will call methods from classes, that mapping each web page accessed during the tests.




