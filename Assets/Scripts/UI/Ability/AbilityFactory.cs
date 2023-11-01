using AbilitiesWindow;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AbilitiesWindow.AbilityUI;

public class AbilityFactory
{
    private Dictionary<AbilityUIModel, Vector2Int> abilitiesGridPositions = new Dictionary<AbilityUIModel, Vector2Int>();

    private List<Vector2Int> descendantsRelativePositions = new List<Vector2Int>();
    private HashSet<AbilityConfig> usedAbilityConfigs = new HashSet<AbilityConfig>();
    private HashSet<AbilityUIModel> abilityModels = new HashSet<AbilityUIModel>();

    private readonly Transform abilitiesRoot;
    private readonly Transform connectionsRoot;
    private readonly int distanceBetweenAbilities;
    private readonly AbilityConnectionView abilityConnectionViewPrefab;
    private readonly AbilityConfig startAbilityConfig;
    private readonly Vector2 startPosition;

    public AbilityFactory(
        Transform abilitiesRoot,
        Transform connectionsRoot,
        AbilityConfig startAbilityConfig,
        int distanceBetweenAbilities,
        AbilityConnectionView abilityConnectionViewPrefab,
        Vector2 startPosition)
    {
        this.abilitiesRoot = abilitiesRoot;
        this.connectionsRoot = connectionsRoot;
        this.startAbilityConfig = startAbilityConfig;
        this.distanceBetweenAbilities = distanceBetweenAbilities;
        this.abilityConnectionViewPrefab = abilityConnectionViewPrefab;
        this.startPosition = startPosition;

        FillDescendantRelativePositions();
    }

    public IEnumerable<AbilityUIModel> Create()
    {
        CreateAbilityAndDescendantsFromConfig(startAbilityConfig);
        FillModelsWithDescendantModels();
        CreateAbilityConnectionViews();

        return abilitiesGridPositions.Keys;
    }

    private void FillDescendantRelativePositions()
    {
        for (var i = -1; i < 2; i++)
            for (var j = -1; j < 2; j++)
                if (i != 0 || j != 0)
                    descendantsRelativePositions.Add(new Vector2Int(i * 2, j));
    }

    private void CreateAbilityAndDescendantsFromConfig(AbilityConfig config, Vector2Int? parentGridCoordinates = null)
    {
        Vector2Int? gridPosition = null;

        if (parentGridCoordinates == null)
        {
            gridPosition = Vector2Int.zero;
        }
        else
        {
            gridPosition = GetNearestFreeGridPosition(parentGridCoordinates);
        }

        if (!gridPosition.HasValue)
        {
            Debug.Log($"No place for ability {config.Id}");
            return;
        }

        var isStart = parentGridCoordinates == null;
        var abilityModel = new AbilityUIModel(config.Id, config.Cost, isStart);

        var abilityView = Object.Instantiate(config.ViewPrefab, abilitiesRoot);

        abilityView.transform.position = GetScreenPositionByGridPosition(gridPosition.Value);
        abilityView.SetText(config.Id);

        var abilityPresenter = new AbilityUIPresenter(abilityModel, abilityView);
        abilityPresenter.Initialize();

        usedAbilityConfigs.Add(config);
        abilitiesGridPositions.Add(abilityModel, gridPosition.Value);
        abilityModels.Add(abilityModel);

        foreach (var descendantConfig in config.DescendantConfigs)
        {
            if (!abilitiesGridPositions.Keys.Any(model => model.Id == descendantConfig.Id))
            {
                CreateAbilityAndDescendantsFromConfig(descendantConfig, gridPosition.Value);
            }
        }
    }

    private void FillModelsWithDescendantModels()
    {
        foreach (var model in abilityModels)
        {
            var config = usedAbilityConfigs.FirstOrDefault(config => config.Id == model.Id);

            foreach (var possibleDescendantModel in abilityModels)
            {
                if (config.DescendantsIds.Contains(possibleDescendantModel.Id))
                {
                    model.AddDescendantModel(possibleDescendantModel);
                }
            }
        }
    }

    private void CreateAbilityConnectionViews()
    {
        foreach (var model in abilityModels)
        {
            if (model.DescendantModels != null)
            {
                var abilityPosition = GetScreenPositionByGridPosition(abilitiesGridPositions[model]);

                foreach (var descendant in model.DescendantModels)
                {
                    var connection = Object.Instantiate(abilityConnectionViewPrefab, connectionsRoot);
                    var descendantPosition = GetScreenPositionByGridPosition(abilitiesGridPositions[descendant]);

                    connection.SetCoordinates(abilityPosition, descendantPosition);
                }
            }
        }
    }

    private Vector2 GetScreenPositionByGridPosition(Vector2Int gridPosition)
    {
        return startPosition - (Vector2)gridPosition * distanceBetweenAbilities;
    }

    private Vector2Int? GetNearestFreeGridPosition(Vector2Int? parentGridPosition)
    {
        Vector2Int lastUsedRelativePosition;
        var counter = 0;

        Vector2Int? freeGridPosition = null;
        while (freeGridPosition == null && counter < descendantsRelativePositions.Count)
        {
            lastUsedRelativePosition = descendantsRelativePositions[counter++];
            var possibleGridPosition = parentGridPosition + lastUsedRelativePosition;

            if (!abilitiesGridPositions.Values.Any(value => value == possibleGridPosition))
            {
                freeGridPosition = possibleGridPosition;
            }
        }

        return freeGridPosition;
    }
}