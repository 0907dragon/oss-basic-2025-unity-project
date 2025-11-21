using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    void update()
    {
        transform.Rotate(Vector3.right * 30 * Time.deltaTime);
    }
}
