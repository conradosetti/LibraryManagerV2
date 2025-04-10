﻿namespace LibraryManager.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public List<Loan> Loans { get; private set; } = [];

    public User(string name, string email) : base()
    {
        Name = name;
        Email = email;
    }
}