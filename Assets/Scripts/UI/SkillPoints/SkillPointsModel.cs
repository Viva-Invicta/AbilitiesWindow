using System;

namespace AbilitiesWindow.SkillPoints
{
    public class SkillPointsModel : Model
    {
        public event Action CountUpdated;

        private int count;

        public SkillPointsModel(int count)
        {
            this.count = count;
        }

        public int Count => count;

        public void Add(int amount)
        {
            count += amount;

            CountUpdated?.Invoke();
        }

        public void Consume(int amount) 
        {
            count -= amount;

            CountUpdated?.Invoke();
        }
    }
}