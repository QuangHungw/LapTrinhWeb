using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020042.DomainModels
{
    /// &lt;summary&gt;
    /// Thông tin tài khoản trong CSDL
    /// &lt;/summary&gt;
    public class UserAccount
    {
        /// &lt;summary&gt;
        /// ID tài khoản
        /// &lt;/summary&gt;
        public string UserID { get; set; } = "";
        /// &lt;summary&gt;
        /// Tên đăng nhập
        /// &lt;/summary&gt;
        public string UserName { get; set; } = "";
        /// &lt;summary&gt;
        /// Tên đầy đủ (tên hiển thị)
        /// &lt;/summary&gt;
        public string FullName { get; set; } = "";
        /// &lt;summary&gt;
        /// Email
        /// &lt;/summary&gt;
        public string Email { get; set; } = "";
        /// &lt;summary&gt;
        /// Đường dẫn file ảnh
        /// &lt;/summary&gt;
        public string Photo { get; set; } = "";
        /// &lt;summary&gt;
        /// Mật khẩu
        /// &lt;/summary&gt;
        public string Password { get; set; } = "";
        /// &lt;summary&gt;
        /// Chuỗi các quyền của tài khoản, phân cách bởi dấu phẩy
        /// &lt;/summary&gt;
        public string RoleNames { get; set; } = "";
    }
}

