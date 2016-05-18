using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DesafioFCamara.Models
{
    [DataContract]
    public class MyToken
    {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public DateTime TimeCreated { get; set; }

        public MyToken(string token, DateTime date)
        {
            Token = token;
            TimeCreated = date;
        }
    }
}