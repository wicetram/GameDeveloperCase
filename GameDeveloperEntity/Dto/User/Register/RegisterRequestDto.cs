﻿using GameDeveloperEntity.Abstract;

namespace GameDeveloperEntity.Dto.User.Register
{
    public class RegisterRequestDto : IDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool PVP { get; set; }
    }
}
