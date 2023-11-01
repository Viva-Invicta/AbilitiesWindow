using AbilitiesWindow.AbilityUI;
using AbilitiesWindow.SkillPoints;
using System.Collections.Generic;
using System.Linq;

namespace AbilitiesWindow
{
    public class AbilityStatusCalculator
    {
        private readonly SkillPointsModel skillPointsModel;
        private IEnumerable<AbilityUIModel> abilityModels;
        private AbilityUIModel startAbility;

        public AbilityStatusCalculator(SkillPointsModel skillPointsModel)
        {
            this.skillPointsModel = skillPointsModel;
        }

        public void SetAbilitiesModels(IEnumerable<AbilityUIModel> abilityModels)
        {
            this.abilityModels = abilityModels;
            startAbility = abilityModels.FirstOrDefault(ability => ability.IsStart);
        }

        //We can learn ability, if there any learned parent ability
        //and we have enough points for it 
        public bool CanLearnAbility(AbilityUIModel abilityModel)
        {
            if (abilityModel.IsLearned)
            {
                return false;
            }

            var learnedParentAbilitiesModels = new HashSet<AbilityUIModel>();
            
            foreach (var model in abilityModels)
            {
                if (model.DescendantModels == null)
                {
                    continue;
                }

                if (model.DescendantModels.Contains(abilityModel) && model.IsLearned)
                {
                    learnedParentAbilitiesModels.Add(model);
                }
            }
            return skillPointsModel.Count >= abilityModel.Cost && learnedParentAbilitiesModels.Any();
        }

        //We can forget ability, if it has no learned descendants, or if there is other way to its 
        //learned descendants
        public bool CanForgetAbility(AbilityUIModel abilityModel)
        {
            if (!abilityModel.IsLearned || abilityModel.IsStart)
            {
                return false;
            }

            var descendants = abilityModel.DescendantModels;
            if (descendants == null)
            {
                return true;
            }
            var learnedDescendants = descendants.Where(descendant => descendant.IsLearned);

            if (!learnedDescendants.Any())
            {
                return true;
            }

            var noOtherWayToAnyLearnedDescendant = false;
            foreach (var descendant in learnedDescendants)
            {
                if (!CheckOtherWaysToDescendant(startAbility, abilityModel, descendant))
                {
                    noOtherWayToAnyLearnedDescendant = true;
                    break;
                }
            }
            return !noOtherWayToAnyLearnedDescendant;
        }

        //Kind of breadth first search :o
        private bool CheckOtherWaysToDescendant(AbilityUIModel startAbility, AbilityUIModel excludeFromWay, AbilityUIModel targetAbility)
        {
            var abilitiesToCheck = new List<AbilityUIModel>();

            foreach (var descendant in startAbility.DescendantModels)
            {
                if (descendant.IsLearned && descendant != excludeFromWay)
                {
                    abilitiesToCheck.Add(descendant);
                }
            }

            while (abilitiesToCheck.Count > 0)
            {
                if (abilitiesToCheck.Contains(targetAbility))
                {
                    return true;
                }

                abilitiesToCheck.Clear();
                foreach (var ability in abilitiesToCheck)
                {
                    foreach (var descendant in ability.DescendantModels)
                    {
                        if (descendant.IsLearned && descendant != excludeFromWay)
                        {
                            abilitiesToCheck.Add(descendant);
                        }
                    }
                }
            }
            return false;
        }
    }
}