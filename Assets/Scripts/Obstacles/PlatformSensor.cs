using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSensor : MonoBehaviour
{

    [SerializeField] Transform platformTransform;
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = platformTransform;
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }
}
