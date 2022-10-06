using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLine : MonoBehaviour
{
    [SerializeField] Transform player;
    LineRenderer lineRenderer;

    public void Initialize()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        AddPoint(player.position);
    }

    public void StopLine()
    {
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (lineRenderer.positionCount > 0)
        {
            lineRenderer.SetPosition(0, player.position);
            CheckDistanceToTarget();
        }
    }

    private void CheckDistanceToTarget()
    {
        if (lineRenderer.positionCount > 1 && Vector2.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1)) < 0.1f)
        {
            DeleteFirstPoint();
        }
    }

    public void AddPoint(Vector2 newPosition)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
    }

    public void DeleteFirstPoint()
    {
        if (lineRenderer.positionCount > 1)
        {
            for (int i = 0; i < lineRenderer.positionCount - 1; i++)
            {
                lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
            }
            lineRenderer.positionCount--;
        }
    }
}
