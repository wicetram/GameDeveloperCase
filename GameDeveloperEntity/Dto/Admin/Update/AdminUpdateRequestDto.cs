﻿namespace GameDeveloperEntity.Dto.Admin.Update
{
    public class AdminUpdateRequestDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool Active { get; set; }
    }
}
