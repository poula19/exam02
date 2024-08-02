using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace exam02
{

    class Program
    {
        static void Main(string[] args)
        {
            Subject subject = CreateSubjectAndExam();

            Console.Clear(); // Clear the console after exam creation

            // Simulate taking the exam
            int totalScore = 0;
            int totalPossibleScore = 0;
            List<TimeSpan> answerTimes = new List<TimeSpan>();
            List<int> userAnswers = new List<int>();

            Console.WriteLine($"Starting {subject.SubjectName} Exam\n");

            Stopwatch stopwatch = new Stopwatch();

            foreach (var question in subject.Exam.Questions)
            {
                Console.WriteLine(question);
                for (int i = 0; i < question.AnswerList.Count; i++)
                {
                    Console.WriteLine(question.AnswerList[i]);
                }

                stopwatch.Restart();
                Console.Write("\nEnter your answer (1, 2, or 3): ");
                int userAnswer = int.Parse(Console.ReadLine());
                stopwatch.Stop();

                userAnswers.Add(userAnswer);
                answerTimes.Add(stopwatch.Elapsed);

                if (question.AnswerList[userAnswer - 1].AnswerId == question.CorrectAnswer.AnswerId)
                {
                    totalScore += question.Mark;
                }
                totalPossibleScore += question.Mark;

                Console.WriteLine();
            }

            // Display final results
            Console.Clear();
            Console.WriteLine("Exam completed. Here are your results:\n");

            Console.WriteLine($"Subject: {subject.SubjectName}");
            Console.WriteLine($"Exam Type: {(subject.Exam is FinalExam ? "Final" : "Practical")}");
            Console.WriteLine($"Total Questions: {subject.Exam.NumberOfQuestions}");
            Console.WriteLine($"Time Limit: {subject.Exam.Time}\n");

            for (int i = 0; i < subject.Exam.Questions.Count; i++)
            {
                var question = subject.Exam.Questions[i];
                Console.WriteLine($"Question {i + 1}: {question.Body}");
                Console.WriteLine($"Your Answer: {userAnswers[i]}");
                Console.WriteLine($"Correct Answer: {question.CorrectAnswer.AnswerId}");
                Console.WriteLine($"Time Taken: {answerTimes[i].TotalSeconds:F2} seconds");
                Console.WriteLine($"Mark: {(question.AnswerList[userAnswers[i] - 1].AnswerId == question.CorrectAnswer.AnswerId ? question.Mark : 0)}/{question.Mark}");
                Console.WriteLine();
            }

            Console.WriteLine($"\nYour score: {totalScore}/{totalPossibleScore}");
            double percentage = (double)totalScore / totalPossibleScore * 100;
            Console.WriteLine($"Percentage: {percentage:F2}%");

            TimeSpan totalTime = new TimeSpan(answerTimes.Sum(t => t.Ticks));
            Console.WriteLine($"Total Time Taken: {totalTime.TotalMinutes:F2} minutes");
        }

        static Subject CreateSubjectAndExam()
        {
            Console.WriteLine("Enter subject ID:");
            int subjectId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter subject name:");
            string subjectName = Console.ReadLine();
            Subject subject = new Subject(subjectId, subjectName);

            Console.WriteLine("Enter exam type (1 for Final, 2 for Practical):");
            bool isFinal = int.Parse(Console.ReadLine()) == 1;
            Console.WriteLine("Enter exam duration in minutes:");
            int durationMinutes = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter number of questions:");
            int numberOfQuestions = int.Parse(Console.ReadLine());

            subject.CreateExam(isFinal, TimeSpan.FromMinutes(durationMinutes), numberOfQuestions);

            for (int i = 0; i < numberOfQuestions; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1}:");
                string header;
                Question question;

                if (isFinal)
                {
                    Console.WriteLine("Enter question type (1 for True/False, 2 for MCQ):");
                    int questionType = int.Parse(Console.ReadLine());
                    header = questionType == 1 ? "True/False" : "MCQ";
                }
                else
                {
                    header = "MCQ";
                }

                Console.WriteLine("Enter question body:");
                string body = Console.ReadLine();
                Console.WriteLine("Enter question mark:");
                int mark = int.Parse(Console.ReadLine());

                if (header == "True/False")
                {
                    question = new TrueFalseQuestion(header, body, mark);
                    Console.WriteLine("Enter correct answer (1 for True, 2 for False):");
                    int correctAnswer = int.Parse(Console.ReadLine());
                    question.CorrectAnswer = question.AnswerList[correctAnswer - 1];
                }
                else
                {
                    question = new MCQQuestion(header, body, mark);
                    for (int j = 0; j < 3; j++)
                    {
                        Console.WriteLine($"Enter option {j + 1}:");
                        string optionText = Console.ReadLine();
                        question.AnswerList.Add(new Answer(j + 1, optionText));
                    }
                    Console.WriteLine("Enter the number of the correct answer (1, 2, or 3):");
                    int correctAnswer = int.Parse(Console.ReadLine());
                    question.CorrectAnswer = question.AnswerList[correctAnswer - 1];
                }

                subject.Exam.Questions.Add(question);
            }

            return subject;
        }
    }
}

