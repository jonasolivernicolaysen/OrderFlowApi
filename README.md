# OrderFlowApi

OrderFlowApi is an ASP.NET Core Web API for managing products, customer orders, and payments, tested with end-to-end integration tests.

Rather than trying to cover every possible feature, the project is intentionally focused on the fundamentals: clean service-layer architecture, predictable order state transitions, and reliable integration tests that exercise the full HTTP pipeline. 
Authentication is ignored and Authorization is made as simple as possible.

## Overview

The API is built with ASP.NET Core and Entity Framework Core and models a simple commerce-style workflow:

Products can be created, updated, and deleted

Users can create orders for products

Orders move through a defined lifecycle

Payments are processed with idempotent behavior

Access is restricted to the owning user

All business rules are enforced in the service layer and validated through integration tests using real HTTP requests.

## Features

Product lifecycle management

Order creation, update, cancellation, and payment

Explicit order state machine with enforced invariants

Per-user authorization boundaries

Idempotent payment handling

DTO-based request/response models

Global exception handling middleware

Integration tests covering:

Order creation and payment flow

Invalid state transitions

Unauthorized access

Missing resources

## Architecture
API

Controllers

DTOs and mapping

Exception handling middleware

Domain / Services

Order lifecycle rules

Authorization checks

Validation and invariants

Infrastructure

Entity Framework Core

SQLite database (in-memory for tests)

## Testing

Integration tests using WebApplicationFactory

Real HTTP requests against an in-memory server

Full application behavior

Full order lifecycle

## Tech Stack
C#

ASP.NET Core

Entity Framework Core

SQLite

xUnit

WebApplicationFactory

## Run
dotnet run 

PS: add /swagger to the provided url
