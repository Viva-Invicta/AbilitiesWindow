using AbilitiesWindow.AbilityMap;
using AbilitiesWindow.SkillPoints;
using UnityEngine;

namespace AbilitiesWindow
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AbilityConfig firstAbility;

        [Space(20)]
        [SerializeField] private Transform abilitiesRoot;
        [SerializeField] private Transform connectionsRoot;
        [SerializeField] private Transform firstAbilityPlacement;
        [SerializeField] private Canvas canvas;

        [Space(20)]
        [SerializeField] private AbilityConnectionView connectionViewPrefab;
        [SerializeField] private AbilityMapView mapViewPrefab;
        [SerializeField] private SkillPointsView skillPointsViewPrefab;

        [Space(20)]
        [SerializeField] private int startSkillPointsCount = 10;

        [Space(20)]
        [SerializeField] private int distanceBetweenAbilities = 100;

        private void Start()
        {
            var abilityFactory = new AbilityFactory(
                abilitiesRoot, connectionsRoot, firstAbility,
                distanceBetweenAbilities, connectionViewPrefab, firstAbilityPlacement.position);

            var skillPointsModel = new SkillPointsModel(startSkillPointsCount);
            var skillPointsView = Instantiate(skillPointsViewPrefab, canvas.transform);

            var abilityStatusCalculator = new AbilityStatusCalculator(skillPointsModel);

            var mapModel = new AbilityMapModel(abilityStatusCalculator);
            var mapView = Instantiate(mapViewPrefab, canvas.transform);

            var abilityLearnSkillPointsHandler = new AbilityLearnSkillPointsHandler(mapModel, skillPointsModel);

            var skillPointsPresenter = new SkillPointsPresenter(skillPointsModel, skillPointsView);
            var mapPresenter = new AbilityMapPresenter(mapView, mapModel, abilityFactory);

            skillPointsPresenter.Initialize();
            mapPresenter.Initialize();
        }
    }
}