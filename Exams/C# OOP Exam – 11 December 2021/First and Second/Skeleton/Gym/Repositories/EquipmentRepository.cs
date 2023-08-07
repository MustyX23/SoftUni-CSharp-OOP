using Gym.Models.Athletes;
using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> equipments;

        public EquipmentRepository()
        {
            equipments = new List<IEquipment>();
        }
        public IReadOnlyCollection<IEquipment> Models => equipments.AsReadOnly();

        public void Add(IEquipment equipment)
        {
            equipments.Add(equipment);
        }

        public IEquipment FindByType(string equipmentType)
        {
            return equipments.FirstOrDefault(e => e.GetType().Name == equipmentType);
        }

        public bool Remove(IEquipment equipment)
        {
            if (equipments.Contains(equipment))
            {
                equipments.Remove(equipment);
                return true;
            }
            return false;
        }
    }
}
