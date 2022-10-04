using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5f;
    private Vector2 targetPoint;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            targetPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Vector2.Distance(transform.position, targetPoint) > 0.1f)
        {
            MovePlayer(targetPoint);
        }
    }

    void MovePlayer(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.Translate(direction * playerSpeed * Time.deltaTime);
    }
}
