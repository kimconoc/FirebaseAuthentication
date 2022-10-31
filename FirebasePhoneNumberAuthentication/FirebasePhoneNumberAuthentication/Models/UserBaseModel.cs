using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirebasePhoneNumberAuthentication.Models
{
    public class UserBaseModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int LocationId { get; set; }
        public string Email { get; set; }
    }
}