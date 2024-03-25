using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Blacklist : Entity<Guid>
{
    public Blacklist() { }

    public Blacklist(string reason, DateTime date, Guid applicantId)
    {
        Reason = reason;
        Date = date;
        ApplicantId = applicantId;
    }

    public string Reason { get; set; }
    public DateTime Date { get; set; }
    public Guid ApplicantId { get; set; }
    public virtual Applicant? Applicant { get; set; }
}
