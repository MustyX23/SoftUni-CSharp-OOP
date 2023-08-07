using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const double SubmarineArmorThickness = 200;

        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, SubmarineArmorThickness)
        {
            SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; }

        public override void RepairVessel()
        {
            if (this.ArmorThickness < SubmarineArmorThickness)
            {
                this.ArmorThickness = SubmarineArmorThickness;
            }
        }

        public void ToggleSubmergeMode()
        {
            if (this.SubmergeMode == false)
            {
                this.SubmergeMode = true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }
            else
            {
                this.SubmergeMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            if (this.SubmergeMode == true)
            {
                sb.AppendLine(" *Submerge mode: ON");
            }
            else
            {
                sb.AppendLine(" *Submerge mode: OFF");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
