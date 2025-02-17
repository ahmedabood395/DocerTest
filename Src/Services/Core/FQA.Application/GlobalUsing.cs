﻿global using AutoMapper;
global using FAQ.Infrastructure.Helpers;
global using Domain.Enums;
global using ResponseDto;
global using FAQ.Domain.Models;
global using Infrastructure.GenericRepo;
global using Infrastructure.UnitOfWork;
global using MediatR;
global using FAQ.Application.Features.Commands.PutResolveReport;
global using FAQ.Helpers;
global using FluentValidation;
global using AdminPanel.UserManagement.Protos;
global  using Grpc.Core;
global using Autofac;
global using System.Security.Claims;
global using FAQ.Protos;
global using Google.Protobuf.WellKnownTypes;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc.ActionConstraints;
global using Microsoft.AspNetCore.Mvc.Infrastructure;
global using FAQ.Application.Features.Commands.PostResolveReport;