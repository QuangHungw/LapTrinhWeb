using Microsoft.AspNetCore.Authentication.Cookies;
using SV20T1020042.DomainModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Security.Claims;

namespace SV20T1020042.Web
{
    public class WebUserRole
    {
        /// &lt;summary&gt;
        /// Ctor
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;name&quot;&gt;Tên/ký hiệu nhóm/quyền&lt;/param&gt;
        /// &lt;param name=&quot;description&quot;&gt;Mô tả&lt;/param&gt;
        public WebUserRole(string name, string description)
        {
            Name = name;
            Description = description;
        }
        /// &lt;summary&gt;
        /// Tên/Ký hiệu quyền
        /// &lt;/summary&gt;
        public string Name { get; set; }
        /// &lt;summary&gt;
        /// Mô tả
        /// &lt;/summary&gt;
        public string Description { get; set; }
    }
}
    /// &lt;summary&gt;
    /// Danh sách các nhóm quyền sử dụng trong ứng dụng
    /// &lt;/summary&gt;
    