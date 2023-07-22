using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> heroes)
        {
            List<IHero> knights = heroes.Where(h => h.GetType().Name == "Knight" && h.IsAlive && h.Weapon != null).ToList();
            List<IHero> barbarians = heroes.Where(h => h.GetType().Name == "Barbarian" && h.IsAlive && h.Weapon != null).ToList();

            int initialKnightsCount = knights.Count;
            int initialBarbariansCount = barbarians.Count;

            while (knights.Any() && barbarians.Any())
            {
                KnightsAttackBarbarians(knights, barbarians);
                BarbariansAttackKnights(barbarians, knights);

                knights.RemoveAll(k => k.Health <= 0);
                barbarians.RemoveAll(b => b.Health <= 0);
            }

            if (knights.Any())
            {
                return $"The knights took {initialKnightsCount - knights.Count} casualties but won the battle.";
            }
            else
            {
                return $"The barbarians took {initialBarbariansCount - barbarians.Count} casualties but won the battle.";
            }
        }

        private static void KnightsAttackBarbarians(List<IHero> knights, List<IHero> barbarians)
        {
            foreach (var knight in knights)
            {
                if (knight.IsAlive)
                {
                    foreach (var barbarian in barbarians)
                    {
                        if (barbarian.IsAlive)
                        {
                            int damage = 0;
                            if (knight.Weapon != null)
                            {
                                damage = knight.Weapon.DoDamage();
                            }
                            barbarian.TakeDamage(damage);
                        }
                    }
                }
            }
        }
        private static void BarbariansAttackKnights(List<IHero> barbarians, List<IHero> knights)
        {
            foreach (var barbarian in barbarians)
            {
                if (barbarian.IsAlive)
                {
                    foreach (var knight in knights)
                    {
                        if (knight.IsAlive)
                        {
                            int damage = 0;
                            if (barbarian.Weapon != null)
                            {
                                damage = barbarian.Weapon.DoDamage();
                            }
                            knight.TakeDamage(damage);
                        }
                    }
                }
            }
        }

    }

}  
