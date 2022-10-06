using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5f;
    [SerializeField]
    private PlayerLine line;

    private Vector2 currentTarget;
    private List<Vector2> targets;
    private PlayerGameplay gameplay;

    private void Awake()
    {
        gameplay = GetComponent<PlayerGameplay>();
        targets = new List<Vector2>();
        Initialize();
    }

    public void Initialize()
    {
        targets.Clear();
        currentTarget = transform.position;
    }

    private void Update()
    {
        if (gameplay.GameIsActive)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                AddTarget(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
            }

            if (Input.GetMouseButtonDown(0))
            {
                AddTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

            if (Vector2.Distance(transform.position, currentTarget) > 0.1f)
            {
                MovePlayer(currentTarget);
            }
            else
            {
                TryNextTarget();
            }
        }
    }

    private void AddTarget(Vector3 target)
    {
        line.AddPoint(target);
        targets.Add(target);
    }

    private void MovePlayer(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.Translate(direction * playerSpeed * Time.deltaTime);
    }

    private void TryNextTarget()
    {
        if (targets.Count > 0)
        {
            if (targets.Count > 1)
            {
                targets.RemoveAt(0);
            }
            currentTarget = targets[0];
        }
    }
}
