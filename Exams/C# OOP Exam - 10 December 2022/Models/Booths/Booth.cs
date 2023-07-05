using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved;

        public int BoothId
        {
            get { return boothId; }
            private set { boothId = value; }
        }
        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                capacity = value;
            }
        }
        public IRepository<IDelicacy> DelicacyMenu
        {
            get { return delicacyMenu; }
            private set { delicacyMenu = value; }
        }
        public IRepository<ICocktail> CocktailMenu 
        {
            get { return cocktailMenu; }
            private set { cocktailMenu = value; }
        }
        public double CurrentBill
        {
            get { return currentBill; }
            private set { currentBill = value; }
        }
        public double Turnover
        {
            get { return turnover; }

            private set
            {
                turnover = value;
            }
        }
        public bool IsReserved
        {
            get { return isReserved; }
            private set { isReserved = value; }
        }

        public void ChangeStatus()
        {
            if (IsReserved == true)
            {
                IsReserved = false;
            }
            else
            {
                IsReserved = true;
            }
        }
        public void Charge()
        {
            Turnover += CurrentBill;
            CurrentBill = 0;
        }
        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            DelicacyMenu = new DelicacyRepository();
            CocktailMenu = new CocktailRepository();
            currentBill = 0;
            turnover = 0;
            isReserved = false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:F2} lv");
            sb.AppendLine("-Cocktail menu:");
            foreach (var cocktail in this.CocktailMenu.Models)
            {
                sb.AppendLine($"--{cocktail.ToString()}");
            }
            sb.AppendLine("-Delicacy menu:");
            foreach (var delicacy in DelicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy.ToString()}");
            }

            return sb.ToString();
        }
    }
}
