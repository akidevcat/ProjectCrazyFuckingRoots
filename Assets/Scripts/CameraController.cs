using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;
    public float CastDistance = 200.0f;

    private void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");
        var rotate = Input.GetAxis("Rotate");

        var dirRight = transform.right;
        var dirForward = transform.forward;
        dirForward.y = 0;
        dirForward = dirForward.normalized;
        
        transform.position += dirRight * moveX * MovementSpeed * Time.deltaTime;
        transform.position += dirForward * moveY * MovementSpeed * Time.deltaTime;

        var ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out var hit, CastDistance, int.MaxValue))
        {
            transform.RotateAround(hit.point, Vector3.up, -rotate * RotationSpeed * Time.deltaTime);
        }
    }
}
