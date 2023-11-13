﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public record Contact(
    int ContactId,
    string Name,
    string Address,
    string City
);

public class ContactService
{
    private readonly List<Contact> _contacts = new()
    {
        new(1, "Filip W", "New Street 1", "Zurich"),
        new(2, "Josh Donaldson", "1 Blue Jays Way", "Toronto"),
        new(3, "Aaron Sanchez", "1 Blue Jays Way", "Toronto"),
        new(4, "Jose Bautista", "1 Blue Jays Way", "Toronto"),
        new(5, "Edwin Encarnacion", "1 Blue Jays Way", "Toronto")
    };

    public Task<IEnumerable<Contact>> GetAll()
        => Task.FromResult(_contacts.AsEnumerable());

    public Task<Contact> Get(int id) 
        => Task.FromResult(_contacts.FirstOrDefault(x => x.ContactId == id));

    public Task<int> Add(Contact contact)
    {
        var newId = (_contacts.LastOrDefault()?.ContactId ?? 0) + 1;
        _contacts.Add(contact with { ContactId = newId });
        
        return Task.FromResult(newId);
    }

    public async Task Delete(int id)
    {
        var contact = await Get(id);
        if (contact == null)
        {
            throw new InvalidOperationException($"Contact with id '{id}' does not exists");
        }

        _contacts.Remove(contact);
    }
}