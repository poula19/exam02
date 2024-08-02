using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{

    public class Subject : ICloneable
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; private set; }

        public Subject(int id, string name)
        {
            SubjectId = id;
            SubjectName = name;
        }

        public void CreateExam(bool isFinal, TimeSpan time, int numberOfQuestions)
        {
            Exam = isFinal
                ? new FinalExam(time, numberOfQuestions)
                : (Exam)new PracticalExam(time, numberOfQuestions);
        }

        public object Clone()
        {
            return new Subject(SubjectId, SubjectName)
            {
                Exam = (Exam)this.Exam?.Clone()
            };
        }

        public override string ToString()
        {
            return $"Subject ID: {SubjectId}, Name: {SubjectName}";
        }
    }
}
