using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }
        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject) &&
                subjectType != nameof(HumanitySubject) &&
                subjectType != nameof(TechnicalSubject))
            {
                return $"Subject type {subjectType} is not available in the application!";
            }
           
            if (subjects.Models.Any(s => s.Name == subjectName))
            {
                return $"{subjectName} is already added in the repository.";
            }

            ISubject subject = null;
            int subjectId = subjects.Models.Count + 1;

            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjectId, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjectId, subjectName);
            }
            else if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(subjectId, subjectName);
            }

            subjects.AddModel(subject);
            return $"{subjectType} {subjectName} is created and added to the {subjects.GetType().Name}!";

        }
        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return $"{universityName} is already added in the repository.";
            }
            // Convert requiredSubjects to a list of subject IDs
            List<int> requiredSubjectIds = new List<int>();

            int universityId = universities.Models.Count + 1;
            foreach (string subjectName in requiredSubjects)
            {
                ISubject subject = subjects.FindByName(subjectName);

                if (subject == null)
                {
                    return $"Subject {subjectName} is not available in the SubjectRepository!";
                }

                requiredSubjectIds.Add(subject.Id);
            }

            // Create and add the university

            IUniversity newUniversity = new University(universityId, universityName, category, capacity, requiredSubjectIds);
            universities.AddModel(newUniversity);

            string relevantRepositoryTypeName = typeof(UniversityRepository).Name;
            return $"{universityName} university is created and added to the {relevantRepositoryTypeName}!";

        }
        public string AddStudent(string firstName, string lastName)
        {
            string fullname = firstName + " " + lastName;
            if (students.FindByName(fullname) != null)
            {
                return $"{firstName} {lastName} is already added in the repository.";
            }

            // Create and add the student
            IStudent newStudent = new Student(students.Models.Count + 1, firstName, lastName);
            students.AddModel(newStudent);

            string relevantRepositoryTypeName = typeof(StudentRepository).Name;
            return $"Student {firstName} {lastName} is added to the {relevantRepositoryTypeName}!";
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);

            if (student == null)
            {
                return "Invalid student ID!";
            }

            ISubject subject = subjects.FindById(subjectId);
            if (subject == null)
            {
                return "Invalid subject ID!";
            }

            if (student.CoveredExams.Contains(subjectId))
            {
                return $"{student.FirstName} {student.LastName} has already covered exam of {subject.Name}.";
            }

            student.CoverExam(subject);
            return $"{student.FirstName} {student.LastName} covered {subject.Name} exam!";
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] splittedName = studentName.Split(' ');
            string firstName = splittedName[0];
            string lastName = splittedName[1];

            IStudent student = students.FindByName(studentName);
            if (student == null)
            {
                return $"{firstName} {lastName} is not registered in the application!";
            }
            IUniversity university = universities.FindByName(universityName);
            if (university == null)
            {
                return $"{universityName} is not registered in the application!";
            }


            bool hasCoveredAllExams = CheckIfStudentHasCoveredAllExams(student, university);

            if (!hasCoveredAllExams)
            {
                return $"{studentName} has not covered all the required exams for {universityName} university!";
            }

            if (student.University != null && student.University.Name == universityName)
            {
                return $"{student.FirstName} {student.LastName} has already joined {universityName}.";
            }

            student.JoinUniversity(university);

            return $"{student.FirstName} {student.LastName} joined {universityName} university!";
        }
      
        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            int studentsCount = students.Models.Count(s => s.University != null && s.University.Id == universityId);
            int capacityLeft = university.Capacity - studentsCount;

            StringBuilder report = new StringBuilder();
            report.AppendLine($"*** {university.Name} ***");
            report.AppendLine($"Profile: {university.Category}");
            report.AppendLine($"Students admitted: {studentsCount}");
            report.Append($"University vacancy: {capacityLeft}");

            return report.ToString().Trim();
        }

        private bool CheckIfStudentHasCoveredAllExams(IStudent student, IUniversity university)
        {
            foreach (int requiredSubjectId in university.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(requiredSubjectId))
                {
                    return false;
                }
            }

            return true;
        }
    }
}