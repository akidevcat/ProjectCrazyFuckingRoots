using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;
    public float CastDistance = 200.0f;
    public Vector2 BoxSize;
    public Transform BoxPivot;
    
    private int surfaceLayer;

    private void Awake()
    {
        surfaceLayer = LayerMask.NameToLayer("Surface");
    }
    
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

        var pos2 = new Vector2(transform.position.x, transform.position.z);
        pos2 -= new Vector2(BoxPivot.position.x, BoxPivot.position.z);
        pos2.x = Mathf.Min(pos2.x, BoxSize.x / 2.0f);
        pos2.x = Mathf.Max(pos2.x, -BoxSize.x / 2.0f);
        pos2.y = Mathf.Min(pos2.y, BoxSize.y / 2.0f);
        pos2.y = Mathf.Max(pos2.y, -BoxSize.y / 2.0f);
        pos2 += new Vector2(BoxPivot.position.x, BoxPivot.position.z);

        transform.position = new Vector3(pos2.x, transform.position.y, pos2.y);

        var ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out var hit, CastDistance, 1 << surfaceLayer))
        {
            transform.RotateAround(hit.point, Vector3.up, -rotate * RotationSpeed * Time.deltaTime);
        }
    }
}
