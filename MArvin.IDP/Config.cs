﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MArvin.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles","Your role(s)",["role"]),
            new IdentityResource("country","Your country",["country"])
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("imagegalleryapi", "Image Gallery API",["role", "country"])
            {
                Scopes = { "imagegalleryapi.fullaccess" }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("imagegalleryapi.fullaccess")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "Image Gallery",
                ClientId = "imagegalleryclient",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris =
                {
                    "https://localhost:7184/signin-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "imagegalleryapi.fullaccess",
                    "country"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RequireConsent = true,
                PostLogoutRedirectUris =
                {
                    "https://localhost:7184/signout-callback-oidc"
                }
            }
        };
}