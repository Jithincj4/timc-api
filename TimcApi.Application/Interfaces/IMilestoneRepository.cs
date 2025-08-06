using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimcApi.Domain.Entities;

namespace TimcApi.Application.Interfaces
{
    public interface IMilestoneRepository
    {
        Task<IEnumerable<Milestone>> GetByPatientIdAsync(int patientId);
        Task<int> CreateAsync(Milestone milestone);
        Task UpdateAsync(Milestone milestone);
    }
}
