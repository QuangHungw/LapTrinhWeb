﻿using Microsoft.AspNetCore.Authentication.Cookies;
using SV20T1020042.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Security.Claims;

namespace SV20T1020042.Web
{
    public static class WebUserExtensions
    {
        /// &lt;summary&gt;
        ///
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;principal&quot;&gt;&lt;/param&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public static WebUserData? GetUserData(this ClaimsPrincipal principal)
        {
            try
            {
                if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated)
                    return null;
                var userData = new WebUserData();
                userData.UserId = principal.FindFirstValue(nameof(userData.UserId));
                userData.UserName = principal.FindFirstValue(nameof(userData.UserName));
                userData.DisplayName = principal.FindFirstValue(nameof(userData.DisplayName));
                userData.Email = principal.FindFirstValue(nameof(userData.Email));
                userData.Photo = principal.FindFirstValue(nameof(userData.Photo));
                userData.ClientIP = principal.FindFirstValue(nameof(userData.ClientIP));
                userData.SessionId = principal.FindFirstValue(nameof(userData.SessionId));
                userData.AdditionalData = principal.FindFirstValue(nameof(userData.AdditionalData));
                userData.Roles = new List<string> ();
                foreach (var claim in principal.FindAll(ClaimTypes.Role))
                {
                    userData.Roles.Add(claim.Value);
                }
                return userData;
            }
            catch
            {

                return null;
            }
        }
    }
}
