﻿namespace DatingAppCourse.Api.DTOs
{
    public class UserDto
    {
        public required string UserName { get; set; }
        public required string Token { get; set; }
        public DateTime TokenExpiration { get; set; }

    }
}
