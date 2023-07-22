using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;

        protected Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.WeaponTypeNull);
                }
                name = value;
            }
        }
        public int Durability 
        {
            get => durability;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZero);
                }
                durability = value;
            }
        }

        public abstract int DoDamage();
        protected void DecreaseDurability()
        {
            Durability--;
        }
    }
}
