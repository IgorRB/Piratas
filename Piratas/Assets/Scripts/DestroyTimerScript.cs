using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimerScript : MonoBehaviour
{
    [SerializeField]
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
