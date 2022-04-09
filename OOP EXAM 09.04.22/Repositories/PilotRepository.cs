using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots;
        public PilotRepository()
        {
            this.pilots = new List<IPilot>();
        }
        public IReadOnlyCollection<IPilot> Models
            => this.pilots.AsReadOnly();

        public void Add(IPilot model)
            => this.pilots.Add(model);

        public IPilot FindByName(string name)
            => this.pilots.FirstOrDefault(x => x.FullName == name);

        public bool Remove(IPilot model)
            => this.pilots.Remove(model);
    }
}
