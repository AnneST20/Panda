# PANDA project

## Description

PANDA is a web app that collects ads of rent flats from several sites and gethers them. 
There is also posibility to add custom ads (this  feature is only aviable for admins). 
Ads have only the main info that is displayed and there is map with marked adresses of flats.


## General plan

- Create models:
	- Ad : Id, SourceKey, Url, LastModifed, Price, Adress, AdressLink, Floor, Rooms, Square, Description, Details, Pets, Gallery (Photo collection)
	- Photo : Id, AdId, Url
	- User : (IdentityUser)
	- LikedAd : Id, AdId, UserId
	- Context : Ads, Photos, Users, LikedAds

- Create controllers and repositories:
	- AdsController
		- Add method for photos
	- UsersController
	- Home controller

- Create site mockup and views: 
	- Home page
	- List of ads
	- Opened ad
	- Log in / sign in
	- Liked ads

- Sitemap sites (Rieltor):
	- Create service for reading sitemap
	- Create service for parsing html page

- API sites (OLX):
	- Investigate API
	- ...

- Create jobs and schedulers for ads (each 24 hours):
	- Read ads (API or sitemap) adn generates list of ads
	- Compare with database
	- Refresh database

- To investigate:
	- Map (will be used for the page with ads to mark there adress)
	- Databases (there could be problem with SQL db)
	- RabbitMQ
	- AI for pets


## Scheduled meetings

- 10.03.2023:
	- Models
	- Controllers

- 24.03.2023:
	- Mockup (figma)
	- Rieltor scheduler // change adresslink to coordinates
	- Map investigation // geoJSON


- 14.04.2023:
	- fix scheduler
	- geoJson - elementary implementation - geoJson for all, new filtered geoJsom for search
	- solr for search investigation (create plan for implementing)
	- frontend realization investigation - add comments
	- 


Dapper for db
Solr for db filtration (indexing fields ids)
Separate for user and admin areas
