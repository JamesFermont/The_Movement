using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudienceSpawner : MonoBehaviour {
    [SerializeField] private AudienceMoveSettings moveSettings;
    [SerializeField] private AudienceBehaviorSettings behaveSettings;
    [SerializeField] private List<SpawnConfig> config;

    private Transform _transform;
    
    private void Start() {
        _transform = transform;
        foreach (SpawnConfig spawnConfig in config) {
            if (AudienceBehaviorSettings.GetBehaveCount() == 0) break;
            GameObject parent = new GameObject {
                transform = {
                    position = _transform.position,
                    parent = _transform
                },
                name = spawnConfig.prefab.name
            };
            SpawnAudience(spawnConfig, parent.transform);
        }
    }

    private void SpawnAudience(SpawnConfig spawnConfig, Transform parent) {
        InfluenceHandler.AddCitizens(spawnConfig.quantity);
        var behaveConfig = behaveSettings.GetBehaveConfig();
        for (int i = 0; i < spawnConfig.quantity; i++) {
            Vector3 position = parent.position;
            position.x = Random.Range(-80f, 80f);
            position.y += Random.Range(-0.05f, 0.05f);
            Audience newMember = Instantiate(spawnConfig.prefab, position, Quaternion.identity, parent);
            newMember.SetMoveConfig(moveSettings.GetRandomMoveConfig());
            newMember.SetSettings(behaveConfig);
        }
    }
}

[System.Serializable]
public struct SpawnConfig {
    public Audience prefab;
    public int quantity;
}
