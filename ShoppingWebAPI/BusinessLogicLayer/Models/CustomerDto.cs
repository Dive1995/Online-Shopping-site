using System;
using System.Text.Json.Serialization;

namespace BusinessLogicLayer.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
