using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private readonly List<IFormulaOneCar> bolids;
        public FormulaOneCarRepository()
        {
            this.bolids = new List<IFormulaOneCar>();
        }
        public IReadOnlyCollection<IFormulaOneCar> Models
            => this.bolids.AsReadOnly();

        public void Add(IFormulaOneCar model)
            => this.bolids.Add(model);

        public IFormulaOneCar FindByName(string name)
            => this.bolids.FirstOrDefault(x => x.Model == name);

        public bool Remove(IFormulaOneCar model)
            => this.bolids.Remove(model);
    }
}
