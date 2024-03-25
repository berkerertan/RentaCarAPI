using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities;

public class Bootcamp : Entity<Guid>
{
    public Bootcamp() { }

    public Bootcamp(
        string name,
        Guid ınstructorId,
        Guid bootcampStateId,
        DateTime startDate,
        DateTime endDate,
        BootcampState bootcampState,
        ICollection<ApplicationInfo> applicationInfos,
        Instructor ınstructor
    )
    {
        Name = name;
        InstructorId = ınstructorId;
        BootcampStateId = bootcampStateId;
        StartDate = startDate;
        EndDate = endDate;
        BootcampState = bootcampState;
        ApplicationInfos = applicationInfos;
        Instructor = ınstructor;
    }

    public string Name { get; set; }
    public Guid InstructorId { get; set; }
    public Guid BootcampStateId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public virtual BootcampState? BootcampState { get; set; }
    public virtual ICollection<ApplicationInfo>? ApplicationInfos { get; set; }
    public virtual Instructor? Instructor { get; set; }
}
