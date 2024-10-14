
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyPlatform : MonoBehaviour
{
    private Vector3[] directions =
    {
        new Vector3(0,0,1),
        new Vector3(0,0,-1),
        new Vector3(1,0,0),
        new Vector3(-1,0,0),
        new Vector3(1,0,1),
        new Vector3(-1,0,-1),
        new Vector3(-1,0,1),
        new Vector3(1,0,-1)
    };

    private Vector3 targetDirection;
    [SerializeField] private float speed;
    [SerializeField] private float selectionTime;
    private float time;

    private void Start()
    {
        time = selectionTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        PushPlayer(collision.rigidbody);
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            RandomDirection();
            time = selectionTime;
        }
    }
    private void RandomDirection()
    {
        targetDirection = directions[Random.Range(0, directions.Length)];
    }

    private void PushPlayer(Rigidbody rigidbody)
    {
        rigidbody.AddForce(targetDirection.normalized * speed, ForceMode.VelocityChange);
    }
}
