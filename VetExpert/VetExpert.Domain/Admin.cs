﻿namespace VetExpert.Domain
{
    public class Admin
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public Admin()
        {
            Id = Guid.NewGuid();
        }
    }
}
