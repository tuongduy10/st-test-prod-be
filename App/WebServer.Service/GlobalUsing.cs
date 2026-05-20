global using System.Text;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using Carter;

global using Newtonsoft.Json;
global using Newtonsoft.Json.Serialization;

global using WebServer.Persistence.Extensions;
global using WebServer.Service.Extensions;
global using WebServer.Application.Abstractions.CurrentUser;
global using WebServer.Infrastructure.Extensions;
global using WebServer.Infrastructure.Security;
global using WebServer.Infrastructure.Middlewares;
global using WebServer.Infrastructure.Security.Authorizations;
global using WebServer.Domain.Shared.Constants;
global using WebServer.Domain.Shared.Enums;
global using WebServer.Application.Extensions;
global using WebServer.Api.Extensions;

