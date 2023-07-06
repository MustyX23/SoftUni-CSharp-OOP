using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Repositories
{
    public class BoothRepository : IRepository<IBooth>
    {
        private readonly List<IBooth> booths;
        public IReadOnlyCollection<IBooth> Models => booths;

        public BoothRepository()
        {
            booths = new List<IBooth>();
        }
        public void AddModel(IBooth model)
        {
            this.booths.Add(model);
        }
    }
}
