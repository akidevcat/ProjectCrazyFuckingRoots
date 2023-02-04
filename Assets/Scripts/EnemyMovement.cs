using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Camera mainCamera;

    public Transform moveToPoint;
    void Start()
    {
        moveToPoint = GameObject.Find("Mother Tree").transform;
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(moveToPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }*/
    }
}
