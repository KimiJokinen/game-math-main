using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteMoveScript : MonoBehaviour
{
    [SerializeField] private GameObject concrete;
    [SerializeField] private GameObject hook;
    public bool isAttached = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached == true)
        {
            concrete.transform.position = hook.transform.position - new Vector3(0, 2f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HookAttach")
        {
            isAttached = true;
        }
    }
}
