using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time = 1f;

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(transform.gameObject);
    }
}
