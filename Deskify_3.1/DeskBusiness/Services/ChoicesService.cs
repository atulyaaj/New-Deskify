using DeskData.Repository;
using DeskEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBusiness.Services
{
    public class ChoicesService
    {
        IChoicesRepository _choicesRepository;

        public ChoicesService(IChoicesRepository choicesRepository)
        {
            _choicesRepository = choicesRepository;
        }

        public void AddChoice(Choices choice)
        {
            _choicesRepository.AddChoice(choice);
        }

        public void DeleteChoice(int choiceId)
        {
            _choicesRepository.DeleteChoice(choiceId);
        }

        public void UpdateChoice(Choices choice)
        {
            _choicesRepository.UpdateChoice(choice);
        }

        public Choices GetChoiceById(int choiceId)
        {
            return _choicesRepository.GetChoiceById(choiceId);
        }

        public IEnumerable<Choices> GetAllChoices()
        {
            return _choicesRepository.GetAllChoices();
        }
    }
}
