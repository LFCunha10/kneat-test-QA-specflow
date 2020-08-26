Feature: Filter
	In order to validate new ways to filter my search results
	As a Booking user
	I want to ensure that I can use new filters on my search

Background: 
	Given I am using 'Chrome' browser
	And I am on Booking main page
	And I fill the search fields
	| Field            | Value                                   |
	| Location         | Limerick                                |
	| Dates            | today + 3 months / today + 3 months + 1 |
	| Number of adults | 2                                       |
	| Number of Rooms  | 1                                       |
	And I click on Search button 

@filters @five-star @ star-rating
Scenario: Filter my search by stars
	Given I am on Results Page
	When I click on five star filter
	Then I should be able to validate if 5 star hotel is listed or not
	| Hotel name            | Is Listed |
	| The Savoy Hotel       | Yes       |
	| George Limerick Hotel | No        |

@filters @sauna @spa-wellness-centre
Scenario: Filter my search by "Sauna" option
	Given I am on Results Page
	When I click on Sauna filter
	Then I should be able to validate if hotel is listed or not
	| Hotel name            | Is Listed |
	| Limerick Strand Hotel | Yes       |
	| George Limerick Hotel | No        |


Scenario: If I filter my search by 5 stars room, the results must be rated as 5 stars
	Given I am on Results Page
	When I click on five star filter
	Then I should see five stars in each ad