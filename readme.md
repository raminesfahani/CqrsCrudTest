
# Mc2 CrudTest Project

All of the required patterns and practices are almost done in this project (including: TDD, BDD, DDD, CleanArchitecture and CQRS).
The project can be run in docker mode and IIS both. Front-end project is written based on Blazor which was required on assignment.
The mandatory and optional features would have been done with even few more abilities.



## Architecture Design

This project has been made by following sub-projects:

**Api:** Containing the whole of API endpoints on the server. A swagger is created for this project to see and test the endpoints details easier.

**Core:** this part is including two part which are "Application" and "Domain" projects.
In "Application" project we have implemented all of CRUD operations alongside with validations using MediatR for Clean-Architecture and CQRS (Event-Sourcing).
Also in "Domain" project we've made all of domain entities.

**Infrastructure:** In this part we have persistence database layer which is created and managed by "Ef Code First" and "UnitOfWork".
A generic repository has been made for general usage of entities and every specific rules and operations are defined in each entity's repository.

**Test:** BDD Tests are implemented for CRUD operations considering the project requirements and user behaviors via "Moq", "Shouldly".

**UI:** Front-end project is made so simple and created by Blazor App. It uses the API endpoints to manage the customers.

**Docker-Compose:** This project contains all of the configuration for running docker-compose and containerized the application automatically.

## Database

The selected database is MSSQL Server on Linux docker. All of the migrations will be automatically done after docker-compose runs.
## Installation

You can install and run the project by following code:

```bash
  docker-compose up
```
    