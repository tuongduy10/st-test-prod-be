global using System.Net;
global using System.Text.Json.Serialization;
global using System.Linq.Expressions;
global using System.Security.Claims;
global using System.Text;

global using Newtonsoft.Json;

global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.Extensions.Logging;

global using WebServer.Application.Features.Products.Commands;
global using WebServer.Infrastructure.Security.Authorizations;
global using WebServer.Infrastructure.Middlewares;
global using WebServer.Domain.Shared;
global using WebServer.Domain.Shared.Enums;
global using WebServer.Domain.Abstractions.Errors;
global using WebServer.Domain.Entities;
