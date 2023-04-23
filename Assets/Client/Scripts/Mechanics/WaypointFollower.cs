using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{

    [Header("Follower Parameters")]
    [SerializeField] private FollowPath followPath;
    [SerializeField] private FollowType followType = FollowType.Linear;
    [SerializeField] private float followSpeed = 3f;

    private float distanceOffset = .1f;

    private IEnumerator<Transform> pointInPath;

    private void Start()
    {
        if (followPath == null)
        {
            Debug.LogWarning("Path is not found!");
            return;
        }

        pointInPath = followPath.GetNextPathPoint();

        pointInPath.MoveNext();

        if (pointInPath.Current == null)
        {
            Debug.LogWarning("Next point is not found!");
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    private void Update()
    {
        if (pointInPath == null || pointInPath.Current == null) return;

        if (followType == FollowType.Linear)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, pointInPath.Current.position, followSpeed * Time.deltaTime);
        }
        else if (followType == FollowType.Lerp)
        {
            transform.position = Vector3.Lerp(
                transform.position, pointInPath.Current.position, followSpeed * Time.deltaTime);
        }

        var distanceSqr = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSqr < distanceOffset * distanceOffset)
        {
            pointInPath.MoveNext();
        }
    }

    private enum FollowType
    {
        Linear,
        Lerp
    }
}
