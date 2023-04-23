using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private PathType pathType = PathType.Open;
    [SerializeField] private Transform[] pathPoints;
    
    private int _followDirection = 1;
    private int _followTo = 0;

    private void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Length < 2) return;

        for (int i = 1; i < pathPoints.Length; i++)
        {
            Gizmos.DrawLine(pathPoints[i - 1].position, pathPoints[i].position);
        }

        if (pathType == PathType.Loop)
        {
            Gizmos.DrawLine(pathPoints[0].position, pathPoints[pathPoints.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (pathPoints == null || pathPoints.Length < 1) yield break;

        while(true)
        {
            yield return pathPoints[_followTo];

            if (pathPoints.Length == 1)
            {
                continue;
            }

            if (pathType == PathType.Open)
            {
                if (_followTo <= 0)
                {
                    _followDirection = 1;
                }
                else if (_followTo >= pathPoints.Length - 1)
                {
                    _followDirection = -1;
                }
            }

            _followTo = _followTo + _followDirection;

            if (pathType == PathType.Loop)
            {
                if (_followTo >= pathPoints.Length)
                {
                    _followTo = 0;
                }

                if (_followTo < 0)
                {
                    _followTo = pathPoints.Length - 1;
                }
            }
        }
    }

    private enum PathType
    {
        Open,
        Loop
    }
}
