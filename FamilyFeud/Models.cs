using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FamilyFeud
{

    public class QuestionAnswer
    {
        public string AnswerText { get; set; }
        public int Points { get; set; }
        public int SlotNumber { get; set; }
    }
    public class QuestionSetSummary
    {
        public int QuestionSetId { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class QuestionSet
    {
        public string QuestionText { get; set; }
        public List<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();

        public int TotalPoints => Answers.Sum(a => a.Points);
    }
    internal class Models
    {

    }

}
