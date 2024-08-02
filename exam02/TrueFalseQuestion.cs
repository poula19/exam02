using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string header, string body, int mark) : base(header, body, mark)
        {
            AnswerList.Add(new Answer(1, "True"));
            AnswerList.Add(new Answer(2, "False"));
        }

        public override object Clone()
        {
            return new TrueFalseQuestion(Header, Body, Mark)
            {
                CorrectAnswer = this.CorrectAnswer,
                AnswerList = this.AnswerList.Select(a => new Answer(a.AnswerId, a.AnswerText)).ToList()
            };
        }
    }
}
