using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeToRespawn = 3;
    [SerializeField] private float offsetY = 0.75f;
    [SerializeField] private GameObject player;

    [Header("Camera Target Group")]
    [SerializeField] private GameObject camTargetGroup;
    [SerializeField] private float weightTarget = 1;
    [SerializeField] private float radiusTarget = 3;

    private CinemachineTargetGroup _targetGroupScript;

    private void Awake()
    {
        _targetGroupScript = camTargetGroup.GetComponent<CinemachineTargetGroup>();
    }

    private void Update()
    {
        if (false)
        StartCoroutine(ChangeMap(timeToRespawn));
    }


    private IEnumerator ChangeMap(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject playerIns = Instantiate(player, new Vector3(transform.position.x, transform.position.y + offsetY), 
            player.transform.rotation);

        //playerIns.GetComponent<HealthSystem>().spawner = gameObject;
        //_targetGroupScript.AddMember(playerIns.transform, weightTarget, radiusTarget);

        yield return null;
    }
}
