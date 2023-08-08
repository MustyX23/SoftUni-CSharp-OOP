using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCompetition.Models
{
    public class EconomicalSubject : Subject
    {
        public EconomicalSubject(int subjectId, string subjectName)
            : base(subjectId, subjectName, 1.0)
        {
        }
    }
}
