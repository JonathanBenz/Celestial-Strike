using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeUntilDestroyed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeUntilDestroyed);
    }
}
