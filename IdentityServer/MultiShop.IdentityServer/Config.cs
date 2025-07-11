﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
          new ApiResource("ResourceCatalog"){Scopes = { "CatalogFullPermission" ,"CatalogReadPermission"}},
          new ApiResource("ResourceDiscount"){Scopes = { "DiscountFullPermission"}},
          new ApiResource("ResourceOrder"){Scopes = { "OrderFullPermission"}},
          new ApiResource("ResourceCargo"){Scopes = { "CargoFullPermission"}},
          new ApiResource(IdentityServerConstants.LocalApi.ScopeName) 
        };

        public static IEnumerable<IdentityResource> IdentityResources=> new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogFullPermission", "Full Permission for Catalog API"),
            new ApiScope("CatalogReadPermission", "Read Permission for Catalog API"),
            new ApiScope("DiscountFullPermission", "Full Permission for Discount API"),
            new ApiScope("OrderFullPermission", "Full Permission for Order API"),
            new ApiScope("CargoFullPermission", "Full Permission for Cargo API"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)

        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            //Visitor
            new Client
            {
                ClientId = "MultiShopVisitorId",
                ClientName = "MultiShop Visitor User",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("MultiShopSecret" .Sha256())},
                AllowedScopes = { "DiscountFullPermission" }
            },

            //Manager
            new Client
            {
                ClientId = "MultiShopManagerId",
                ClientName = "MultiShop Manager User",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("MultiShopSecret" .Sha256())},
                AllowedScopes = {"CatalogFullPermission", "CatalogReadPermission" }
            },

            //Admin
            new Client
            {
                ClientId = "MultiShopAdminId",
                ClientName = "MultiShop Admin User",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets={new Secret("MultiShopSecret" .Sha256())},
                AllowedScopes = {"CatalogFullPermission", "CatalogReadPermission", "DiscountFullPermission", 
                    "OrderFullPermission","CargoFullPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 3600,
            }
        };
    }
}