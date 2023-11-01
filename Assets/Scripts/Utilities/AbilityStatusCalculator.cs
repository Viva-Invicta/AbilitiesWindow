using AbilitiesWindow.AbilityUI;
using AbilitiesWindow.SkillPoints;
using System.Collections.Generic;
using System.Linq;

namespace AbilitiesWindow
{
    public class AbilityStatusCalculator
    {
        private readonly SkillPointsModel skillPointsModel;
        private AbilityUIModel startAbilityModel;

        public AbilityStatusCalculator(SkillPointsModel skillPointsModel)
        {
            this.skillPointsModel = skillPointsModel;
        }

        public void SetStartAbility(AbilityUIModel startAbilityModel)
        {
            this.startAbilityModel = startAbilityModel;
        }

        public bool CanLearnAbility(AbilityUIModel abilityModel)
        {
            if (abilityModel.IsLearned)
            {
                return false;
            }

            var hasLearnedNeighbors = abilityModel.NeighborsModels
                .Any(neighborModel => neighborModel.IsLearned);

            return hasLearnedNeighbors && skillPointsModel.Count >= abilityModel.Cost;
        }

        public bool CanForgetAbility(AbilityUIModel abilityModel)
        {
            if (!abilityModel.IsLearned || abilityModel.IsStart)
            {
                return false;
            }

            var learnedNeighbors = abilityModel.NeighborsModels.Where(neighbor => neighbor.IsLearned && !neighbor.IsStart);

            var noOtherWayToAnyLearnedNeighbor = false;
            foreach (var descendant in learnedNeighbors)
            {
                if (!CheckOtherWaysToNeigbor(startAbilityModel, abilityModel, descendant))
                {
                    noOtherWayToAnyLearnedNeighbor = true;
                    break;
                }
            }
            return !noOtherWayToAnyLearnedNeighbor;
        }

        private bool CheckOtherWaysToNeigbor(AbilityUIModel startAbility, AbilityUIModel excludeFromWay, AbilityUIModel targetAbility)
        {
            var currentFrontier = new List<AbilityUIModel>();

            foreach (var neighbor in startAbility.NeighborsModels)
            {
                if (neighbor == targetAbility)
                {
                    return true;
                }
                if (neighbor.IsLearned && neighbor != excludeFromWay)
                {
                    currentFrontier.Add(neighbor);
                }
            }

            var checkedAbilities = currentFrontier;

            while (currentFrontier.Any())
            {
                if (currentFrontier.Contains(targetAbility))
                {
                    return true;
                }

                checkedAbilities = checkedAbilities.Concat(currentFrontier).ToList();
                currentFrontier = currentFrontier
                    .SelectMany(ability => ability.NeighborsModels)
                    .Where(ability => ability.IsLearned && 
                                      ability != excludeFromWay && 
                                      !checkedAbilities.Contains(ability))
                    .ToList();
            }

            return false;
        }
    }
}