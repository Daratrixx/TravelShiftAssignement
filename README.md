# TravelShiftAssignement
This repository contains the sources for the assessement.

The solution contains two projects: ArticleService (Microservice A) and MagazineConnector (Microservice B)

## Microservice B
Microservice B ensure the existence of the database and seeds it. Microservice B presents endpoints for basic CRUD operations for both Articles and Authors, as well as a specialized endpoint for complex requests (pagination, filtering, sorting) on Articles.

Microservice B requires a token to be present on every requests, otherwise it will response with 401 Unauthorized. This behaviour is controlled by a middleware. This is to ensure only Microservice A can use those endpoints.

Microservice B runs on localhost port 8081. Microservice A contacts this URL to access the endpoints.

You can test Microservice B endpoints using Postman. You need the header `Authorization` with the value `2a85c8bc-8112-485d-acb5-1c93db3c4d82`, otherwise the requets will fail.
Here are a few examples:
 * `http://localhost:8081/api/Articles` This will return all articles.
 * `http://localhost:8081/api/Author/2` This will return the profile of one author.
 * `http://localhost:8081/api/Articles/Query` with body `{"count":2, "page":0, "filterField":"idAuthor", "filterValue":"2"}`. This will return only posts written by author#2.
 
 
## Microservice A
Microservice A has a Repository using RestSharp to contact Microservice B. **Unfortunately, I have a Content-Type error that I wasn't able to resolve, making some endpoints from Microservice B inaccessible to A, even if postman works.**

Microservice A has only has one endpoint that works because of the aforementioned error (Get all articles).
There is no restiction on Microservice A endpoints, anyone can make a request on it as long as they have the URL.

There is a demonstration page (Index) that uses ajax to call the endpoints, but only Get all articles works (see error above).

There is an unfinished parameter set prepared for Microservice A, that would have allowed more complexe requests on the endpoints (pagination...)
