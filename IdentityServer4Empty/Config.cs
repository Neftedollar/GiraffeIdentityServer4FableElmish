﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Test;

namespace IdentityServer4Empty
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("testApi", "Test Api Name"), 
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = {"http://localhost:8080/callback.html"},
                    PostLogoutRedirectUris = {"http://localhost:8080"},
                    AllowedCorsOrigins = {"http://localhost:8080"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "testApi"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return  new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username =  "Alice",
                    Password =  "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username =  "Bob",
                    Password =  "password"
                }
            };
        }
    }
}