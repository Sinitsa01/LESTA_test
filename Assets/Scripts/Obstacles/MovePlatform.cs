using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Vector3 targetPosition;
    [SerializeField] private float speed;

    private Vector3 input = new Vector3(0, 0, 1);

    
    private void Start()
    {

        if (startPosition.position.z > pointA.position.z)
        {
            targetPosition = pointB.position;
        }
        else targetPosition = pointA.position;
    }
    private void Update()
    {
        if(transform.position.z <= pointA.position.z)
        {
            targetPosition = pointB.position;
        }

        if (transform.position.z >= pointB.position.z)
        {
            targetPosition = pointA.position;
        }

        if (targetPosition == pointB.position) {
            transform.position += input * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= input * speed * Time.deltaTime;
        }

        

    }
}
