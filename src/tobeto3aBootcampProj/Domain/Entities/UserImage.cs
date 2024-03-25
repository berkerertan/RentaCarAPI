using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class UserImage : Entity<Guid>
{
    public UserImage() { }

    public UserImage(Guid id, Guid userId, string imagePath)
        : this()
    {
        Id = id;
        UserId = userId;
        ImagePath = imagePath;
    }

    public Guid UserId { get; set; }
    public string ImagePath { get; set; }
    public virtual User? User { get; set; }
}
