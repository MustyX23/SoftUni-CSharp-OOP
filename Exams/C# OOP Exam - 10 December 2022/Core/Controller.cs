using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;
            IBooth booth = new Booth(boothId, capacity);
            this.booths.AddModel(booth);
            return String.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }
        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return String.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }
            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return String.Format(OutputMessages.InvalidCocktailSize, size);
            }
            if (this.booths.Models.Any(booth
                => booth.CocktailMenu.Models.Any(coctail => coctail.Name == cocktailName && coctail.Size == size)))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }
            ICocktail cocktail = null;

            if (cocktailTypeName == nameof(Hibernation))
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            if (cocktailTypeName == nameof(MulledWine))
            {
                cocktail = new MulledWine(cocktailName, size);
            }
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.CocktailMenu.AddModel(cocktail);
            return String.Format(OutputMessages.NewCocktailAdded,size, cocktailName, cocktailTypeName);
        }
        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {            
            
            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen))
            {
                return String.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }
            if (this.booths.Models.Any(booth => booth.DelicacyMenu.Models.Any(delicacy => delicacy.Name == delicacyName)))
            {
                return String.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }
            IDelicacy delicacy = null;

            if (delicacyTypeName == nameof(Gingerbread))
            {
                delicacy = new Gingerbread(delicacyName);
            }
            if (delicacyTypeName == nameof(Stolen))
            {
                delicacy = new Stolen(delicacyName);
            }
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.DelicacyMenu.AddModel(delicacy);
            return String.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            return booth.ToString().Trim();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");

            booth.Charge();
            booth.ChangeStatus();

            sb.AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            IBooth booth = this.booths.Models
                .Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .FirstOrDefault();

            if (booth == null)
            {
                return String.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            else
            {
                booth.ChangeStatus();
                return String.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
            }
        }
        public string TryOrder(int boothId, string order)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            if (booth == null)
            {
                return $"Booth with ID {boothId} does not exist!";
            }

            string[] orderDetails = order.Split('/');
            if (orderDetails.Length < 3)
            {
                return "Invalid order format!";
            }

            string itemTypeName = orderDetails[0];
            string itemName = orderDetails[1];
            int count;
            if (!int.TryParse(orderDetails[2], out count))
            {
                return "Invalid count format!";
            }

            if (itemTypeName != nameof(Hibernation)
                && itemTypeName != nameof(MulledWine)
                && itemTypeName != nameof(Stolen)
                && itemTypeName != nameof(Gingerbread))
            {
                return $"{itemTypeName} is not a recognized type!";
            }

            if (itemTypeName != nameof(Hibernation)
                && itemTypeName != nameof(MulledWine))
            {
                if (orderDetails.Length < 4)
                {
                    return "Invalid order format for cocktail!";
                }

                string size = orderDetails[3];
                if (!IsValidCocktailSize(size))
                {
                    return $"Invalid size: {size}!";
                }

                ICocktail cocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == size);
                if (cocktail == null)
                {
                    return $"There is no {size} {itemName} available!";
                }

                double price = cocktail.Price;
                double totalAmount = price * count;
                booth.UpdateCurrentBill(totalAmount);

                return $"Booth {boothId} ordered {count} {itemName}!";
            }
            else if (itemTypeName != nameof(Stolen)
                && itemTypeName != nameof(Gingerbread))
            {
                IDelicacy delicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == itemName);
                if (delicacy == null)
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }

                double price = delicacy.Price;
                double totalAmount = price * count;
                booth.UpdateCurrentBill(totalAmount);

                return $"Booth {boothId} ordered {count} {itemName}!";
            }

            return string.Empty;
        }

        private bool IsValidCocktailSize(string size)
        {
            return size == "Small" || size == "Middle" || size == "Large";
        }
    }
}
