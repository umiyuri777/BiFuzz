using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor1 : MonoBehaviour
{
    private Animator animator;
    private Vector3 doorPosition;
    public Transform target;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        
        doorPosition = this.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        
        animator.SetFloat("Distance", (doorPosition - target.position).magnitude);
    }
}
