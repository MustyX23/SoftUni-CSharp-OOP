using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityCompetition.Models
{
    public class HumanitySubject : Subject
    {
        public HumanitySubject(int subjectId, string subjectName)
            : base(subjectId, subjectName, 1.15)
        {
        }
    }
}
