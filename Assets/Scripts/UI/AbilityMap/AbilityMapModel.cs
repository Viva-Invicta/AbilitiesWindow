using System;
using System.Collections.Generic;
using AbilitiesWindow.AbilityUI;

namespace AbilitiesWindow.AbilityMap
{
    public class AbilityMapModel : Model
    {
        public event Action<AbilityUIModel> AbilityLearned;
        public event Action<AbilityUIModel> AbilityForgotten;

        public event Action AbilitySelected;

        private AbilityUIModel selectedAbilityModel;
        private IEnumerable<AbilityUIModel> abilityModels;

        private readonly AbilityStatusCalculator abilityStatusCalculator;

        public AbilityMapModel(AbilityStatusCalculator abilityStatusCalculator)
        {
            this.abilityStatusCalculator = abilityStatusCalculator;
        }

        public AbilityUIModel SelectedAbilityModel
        {
            get => selectedAbilityModel;
            set
            {
                if (selectedAbilityModel != null && selectedAbilityModel != value)
                {
                    selectedAbilityModel.IsSelected = false;
                }
                selectedAbilityModel = value;
                AbilitySelected?.Invoke();
            }
        }

        public void UpdateAbilitiesStatus()
        {
            SelectedAbilityModel = null;

            UpdateAbilitiesCanLearnStatus();
            UpdateAbilitiesCanForgetStatus();
        }

        public void SetAbilityModels(IEnumerable<AbilityUIModel> models)
        {
            abilityModels = models;

            foreach (var model in abilityModels)
            {
                if (model.IsStart)
                {
                    model.IsLearned = true;
                    abilityStatusCalculator.SetStartAbility(model);
                }
                else
                {
                    model.IsLearned = false;
                }
                model.Selected += () => HandleAbilitySelected(model);
            }

            UpdateAbilitiesStatus();
        }

        public void ForgetSelectedAbility()
        {
            if (SelectedAbilityModel != null)
            {
                SelectedAbilityModel.IsLearned = false;
                AbilityForgotten?.Invoke(selectedAbilityModel);

                UpdateAbilitiesStatus();
            }
        }

        public void ForgetAllAbilities()
        {
            foreach (var abilityModel in abilityModels)
            {
                if (!abilityModel.IsStart && abilityModel.IsLearned)
                {
                    abilityModel.IsLearned = false;
                    AbilityForgotten?.Invoke(abilityModel);
                }
            }

            UpdateAbilitiesStatus();
        }

        public void LearnSelectedAbility()
        {
            if (SelectedAbilityModel != null)
            {
                SelectedAbilityModel.IsLearned = true;
                AbilityLearned?.Invoke(SelectedAbilityModel);

                UpdateAbilitiesStatus();
            }
        }

        private void HandleAbilitySelected(AbilityUIModel ability)
        {
            SelectedAbilityModel = ability;
        }

        private void UpdateAbilitiesCanLearnStatus()
        {
            foreach (var abilityModel in abilityModels)
            {
                abilityModel.CanLearn = abilityStatusCalculator.CanLearnAbility(abilityModel);
            }
        }

        private void UpdateAbilitiesCanForgetStatus()
        { 
            foreach (var abilityModel in abilityModels)
            {
                abilityModel.CanForget = abilityStatusCalculator.CanForgetAbility(abilityModel);
            }
        }
    }
}