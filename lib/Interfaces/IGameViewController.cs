using System.Drawing;

namespace lib.Interfaces
{
    public interface IGameViewController
    {
        void SetQuest(Question quest);
        bool End(bool fromView = false);
        void ChangedTeamInformation(Team[] teams);
        void SetTime(int remainTime);
       void ShowRightAnswer(string currentQuestRightAnswer);
    }
}