using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Entity : MonoBehaviour
{
    public void OnEnd()
    {
        Destroy(gameObject);
    }
}
