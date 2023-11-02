using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrainMovementComponent : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float wayPointTolerance = 0.1f;
   // [SerializeField] Transform targetTransform;

    [SerializeField] Transform[] wayPoints;
    int currentWayPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsWayPoint();

        RotateTowardsWayPoint();

        Vector3 currentPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPosition = new Vector3(wayPoints[currentWayPoint].position.x, 0, wayPoints[currentWayPoint].position.z);

        float distance = Vector3.Distance(currentPosition, targetPosition);

        if (distance < wayPointTolerance)
        {
            SetNextWayPoint();
        }
    }

    void MoveTowardsWayPoint()
    {
        //float step = 3 * Time.deltaTime;
        //Vector3 follow = Vector3.MoveTowards(transform.position, wayPoints[currentWayPoint].position, step);
        //transform.position = new Vector3(follow.x, transform.position.y, follow.z);
        var dir = new Vector3(wayPoints[currentWayPoint].position.x - transform.position.x, transform.position.y, wayPoints[currentWayPoint].position.z - transform.position.z).normalized;
        // Then add the direction * the speed to the current position:
        transform.position += dir * speed * Time.deltaTime;
    }

    void RotateTowardsWayPoint()
    {
        Quaternion lookRotation = Quaternion.LookRotation((wayPoints[currentWayPoint].position - transform.position).normalized);
        Quaternion yRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, yRotation, speed * Time.deltaTime);
    }

    void SetNextWayPoint()
    {
        if(currentWayPoint == wayPoints.Length-1)
        {
            currentWayPoint = 0;
        }
        else
        {
            currentWayPoint++;
        }

    }


}
