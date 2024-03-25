using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ApplicationInfo : Entity<Guid>
{
    public ApplicationInfo() { }

    public ApplicationInfo(
        Guid applicantId,
        Guid bootcampId,
        Guid applicationStateId,
        Applicant applicant,
        Bootcamp bootcamp,
        ApplicationState applicationState
    )
    {
        ApplicantId = applicantId;
        BootcampId = bootcampId;
        ApplicationStateId = applicationStateId;
        Applicant = applicant;
        Bootcamp = bootcamp;
        ApplicationState = applicationState;
    }

    public Guid ApplicantId { get; set; }
    public Guid BootcampId { get; set; }
    public Guid ApplicationStateId { get; set; }
    public virtual Applicant? Applicant { get; set; }
    public virtual Bootcamp? Bootcamp { get; set; }
    public virtual ApplicationState? ApplicationState { get; set; }
}
