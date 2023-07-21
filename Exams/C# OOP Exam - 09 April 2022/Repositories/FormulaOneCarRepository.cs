using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private List<IFormulaOneCar> formulaOneCars;

        public FormulaOneCarRepository()
        {
            formulaOneCars = new List<IFormulaOneCar>();
        }
        public IReadOnlyCollection<IFormulaOneCar> Models => formulaOneCars.AsReadOnly();

        public void Add(IFormulaOneCar model)
        {
            formulaOneCars.Add(model);
        }
        public bool Remove(IFormulaOneCar model)
        {
            IFormulaOneCar car = formulaOneCars.FirstOrDefault(f => f == model);
            if (car != null)
            {
                formulaOneCars.Remove(car);
                return true;
            }
            return false;

        }

        public IFormulaOneCar FindByName(string name)
        {
            return formulaOneCars.FirstOrDefault(fcar => fcar.Model == name);
        }

    }
}
