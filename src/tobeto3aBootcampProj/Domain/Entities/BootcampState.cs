using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class BootcampState : Entity<Guid>
{
    public BootcampState() { }

    public BootcampState(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
