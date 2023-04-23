using Cinemachine;
using UnityEngine;

public class SpawnerPlayer : MonoBehaviour
{
    [Header("Camera Target Group")]
    [SerializeField] private GameObject camTargetGroup;
    [SerializeField] private float weightTarget = 1;
    [SerializeField] private float radiusTarget = 7;
    [SerializeField] private GameObject playerPrefab;

    public void RespawnPlayer()
    {
        camTargetGroup.GetComponent<CinemachineTargetGroup>().AddMember(
            SpawnPlayer().transform, weightTarget, radiusTarget);
    }

    private GameObject SpawnPlayer()
    {
        var spawnPosition = new Vector2(
            transform.position.x, transform.position.y + playerPrefab.gameObject.transform.localScale.y / 2);

        return Instantiate(playerPrefab, spawnPosition, transform.rotation);
    }
}
