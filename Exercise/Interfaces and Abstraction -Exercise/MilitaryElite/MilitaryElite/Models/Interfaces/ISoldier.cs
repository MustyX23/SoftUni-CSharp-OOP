using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models.Interfaces
{
    public interface ISoldier
    {
        //ISoldier should hold id, first name, and last name
        string Id { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}
