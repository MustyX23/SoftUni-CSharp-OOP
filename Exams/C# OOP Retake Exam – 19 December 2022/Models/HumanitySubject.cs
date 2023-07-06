using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCompetition.Models
{
    public class HumanitySubject : Subject
    {
        private const double subjectRate = 1.0;
        public HumanitySubject(int id, string name)
            : base(id, name, subjectRate)
        {
        }
    }
}
