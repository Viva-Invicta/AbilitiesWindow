namespace AbilitiesWindow.SkillPoints
{
    public class SkillPointsPresenter : Presenter<SkillPointsModel, SkillPointsView>
    {
        public SkillPointsPresenter(SkillPointsModel model, SkillPointsView view) : base(view, model)
        { }

        protected override void InitializeInternal()
        {
            view.SetCount(model.Count);

            model.CountUpdated += HandleCountUpdated;
            view.IncreaseButtonClicked += HandleIncreaseButtonClicked;
        }

        private void HandleCountUpdated()
        {
            view.SetCount(model.Count);
        }

        private void HandleIncreaseButtonClicked()
        {
            model.Add(1);
        }
    }
}