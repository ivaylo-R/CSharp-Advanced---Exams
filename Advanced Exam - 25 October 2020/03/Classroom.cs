using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClassroomProject
{
    public class Classroom
    {
        private List<Student> students;

        public Classroom(int capacity)
        {
            this.Capacity = capacity;
            this.students = new List<Student>();
        }

        public int Capacity { get; set; }
        public int Count { get { return students.Count; } }

        public string RegisterStudent(Student student)
        {
            if (this.Capacity > this.students.Count)
            {
                students.Add(student);
                return $"Added student {student.FirstName} {student.LastName}".Trim();
            }
            else
            {
                return $"No seats in the classroom".Trim();
            }
        }

        public string DismissStudent(string firstName, string lastName)
        {
            Student student = students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
            if (student != null)
            {
                students.Remove(student);
                return $"Dismissed student {firstName} {lastName}".Trim();
            }
            return $"Student not found".Trim();
        }

        public string GetSubjectInfo(string subject)
        {
            var studentsBySubject = this.students.Where(s => s.Subject == subject).ToArray();
            if (studentsBySubject.Length == 0)
            {
                return "No students enrolled for the subject".Trim();
            }
            var sb = new StringBuilder();
            sb.AppendLine($"Subject: {subject}");
            sb.AppendLine($"Students:");
            foreach (var student in studentsBySubject)
            {
                sb.AppendLine($"{student.FirstName} {student.LastName}");
            }
            return sb.ToString().Trim();
        }

        public int GetStudentsCount()
        {
            return this.Count;
        }

        public Student GetStudent(string firstName, string lastName)
        {
            return this.students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
        }

    }
}
