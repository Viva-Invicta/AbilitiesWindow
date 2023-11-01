using AbilitiesWindow.AbilityMap;
using AbilitiesWindow.AbilityUI;
using AbilitiesWindow.SkillPoints;

namespace AbilitiesWindow
{
    //This class is created to avoid dependencies between skill points model and ability map model.
    public class AbilityLearnSkillPointsHandler
    {
        private readonly SkillPointsModel skillPointsModel;
        private readonly AbilityMapModel abilityMapModel;

        public AbilityLearnSkillPointsHandler(AbilityMapModel abilityMapModel, SkillPointsModel skillPointsModel)
        {
            this.skillPointsModel = skillPointsModel;
            this.abilityMapModel = abilityMapModel;

            abilityMapModel.AbilityLearned += HandleAbilityLearned;
            abilityMapModel.AbilityForgotten += HandleAbilityForgotten;

            skillPointsModel.CountUpdated += HandleSkillPointsChanged;
        }

        private void HandleAbilityLearned(AbilityUIModel abilityModel)
        {
            skillPointsModel.Consume(abilityModel.Cost);
        }

        private void HandleAbilityForgotten(AbilityUIModel abilityModel)
        {
            skillPointsModel.Add(abilityModel.Cost);
        }

        private void HandleSkillPointsChanged()
        {
            abilityMapModel.UpdateAbilitiesStatus();
        }
    }
}