using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followTime = 0.2f;

    private Vector2 currVelocity;

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 targetPos = Vector2.SmoothDamp(transform.position, target.position, ref currVelocity, followTime);
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }
}
