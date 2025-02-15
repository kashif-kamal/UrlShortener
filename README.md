# URL Shortener

This is a URL Shortener application built with ASP.NET Core. It provides an API to shorten URLs and redirect to the original URLs.

## Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or any other IDE that supports .NET development

## Getting Started

### Clone the Repository
git clone https://github.com/kashif-kamal/UrlShortener.git cd UrlShortener

### Restore Dependencies
dotnet restore

### Update Configuration

Ensure the `appsettings.json` file contains the necessary configuration. Here is an example:
{ "Logging": { "LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" } }, "AllowedHosts": "*", "UrlShortener": { "Domain": "http://short.url" } }

### Run the Application
dotnet run


The application will start and listen on the configured URLs (e.g., `https://localhost:5001` and `http://localhost:5000`).

### Access Swagger UI

Once the application is running, you can access the Swagger UI to interact with the API:
http://localhost:5000/swagger


## Running Tests

To run the tests, use the following command:

dotnet test


## Project Structure

- **Controllers**: Contains the API controllers.
- **Services**: Contains the business logic.
- **Repositories**: Contains the data access logic.
- **Model**: Contains the data models.
- **Middlewares**: Contains custom middleware for exception handling.
- **Tests**: Contains unit tests for the application.

## API Endpoints

### Create Short URL

- **URL**: `/api/UrlShortener`
- **Method**: `POST`
- **Request Body**:
  
  { "originalUrl": "http://example.com" }
  
  - **Response**:
  
  { "shortUrl": "http://short.url/abc123" }
  
  
### Get Original URL

- **URL**: `/api/UrlShortener/{code}`
- **Method**: `GET`
- **Response**: Redirects to the original URL.


## Scalabity
To scale the URL Shortener application for global usage, you need to consider several architectural and infrastructural changes. Here are some key strategies:

### Distributed Caching
Implement a distributed caching solution like NCache, Redis, or Azure Cache for Redis to cache frequently accessed data and reduce database load.

### Database Replication
Use database replication to distribute the database across multiple regions. This ensures high availability and low latency for users worldwide.

### Load Balancing
Use load balancers to distribute incoming traffic across multiple instances of the application. This helps in handling high traffic and provides fault tolerance.

### Content Delivery Network (CDN)
Use a CDN to cache and deliver static content (e.g., Swagger UI, documentation) closer to users, reducing latency and improving load times.

### Cloud Deployment
Deploy the application to a cloud provider like Azure, AWS, or Google Cloud. These platforms offer various services and tools to help with scaling, monitoring, and managing your application.

### Auto-scaling
Configure auto-scaling to automatically adjust the number of running instances based on the current load. This ensures that the application can handle varying traffic levels efficiently.

### Monitoring and Logging
Implement comprehensive monitoring and logging to track the application's performance, detect issues, and make informed decisions about scaling.

