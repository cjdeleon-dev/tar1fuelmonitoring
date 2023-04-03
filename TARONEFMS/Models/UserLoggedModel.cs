using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TARONEFMS.Models
{
    public class UserLoggedModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public byte[] UserPic { get; set; }
    }
}