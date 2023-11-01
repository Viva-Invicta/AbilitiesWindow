using System;
using System.Collections.Generic;

namespace AbilitiesWindow.AbilityUI
{
    public class AbilityUIModel : Model
    {
        public event Action Selected;

        internal event Action SelectionStatusUpdated;
        internal event Action LearnedStatusUpdated;

        private readonly string id;
        private readonly int cost;
        private readonly bool isStart;

        private bool isLearned;
        private bool isSelected;

        public AbilityUIModel(string id, int cost, bool isStart)
        {
            this.id = id;
            this.cost = cost;
            this.isStart = isStart;
        }

        public HashSet<AbilityUIModel> NeighborsModels { get; private set; }
        public HashSet<AbilityUIModel> DescendantModels { get; private set; }
        public bool CanLearn { get; set; }
        public bool CanForget { get; set; }

        public string Id => id;
        public int Cost => cost;
        public bool IsStart => isStart;

        public bool IsLearned 
        {
            get => isLearned;
            set
            {
                if (isLearned != value)
                {
                    isLearned = value;
                    LearnedStatusUpdated?.Invoke();
                }
            }
        }
        
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    SelectionStatusUpdated?.Invoke();

                    if (value)
                    {
                        Selected?.Invoke();
                    }
                }
            }
        }

        public void AddDescendantModel(AbilityUIModel model)
        {
            if (DescendantModels == null)
            {
                DescendantModels = new HashSet<AbilityUIModel>();
            }   
            
            if (!DescendantModels.Contains(model))
            {
                DescendantModels.Add(model);
            }
        }

        public void AddNeighborModel(AbilityUIModel model)
        {
            if (NeighborsModels == null)
            {
                NeighborsModels = new HashSet<AbilityUIModel>();
            }

            if (!NeighborsModels.Contains(model))
            {
                NeighborsModels.Add(model);
            }
        }
    }
}