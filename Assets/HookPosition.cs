using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPosition : MonoBehaviour
{
    [SerializeField] private Transform hookAttach;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = hookAttach.position;
    }
}
