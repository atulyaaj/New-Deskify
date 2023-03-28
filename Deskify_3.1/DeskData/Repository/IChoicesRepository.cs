using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskData.Repository
{
    public interface IChoicesRepository
    {
        void AddChoice(Choices choice);
        void DeleteChoice(int choiceId);
        void UpdateChoice(Choices choice);
        Choices GetChoiceById(int choiceId);
        IEnumerable<Choices> GetAllChoices();
    }
}
