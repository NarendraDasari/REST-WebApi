using System;
using LiteDB;

namespace Api.DomainObjects
{
    public class AppToken
    {
        [BsonId]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
