using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMFlower : MonoBehaviour
{
    public enum State
    {
        BUD,
        SMILE,
    }
    State state;
    private Animator animator;

    public string description;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();


    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    animator.SetTrigger("Bloom");
        //}
    }

    public float ShowBloom()
    {
        if (state == State.BUD)
        {
            animator.SetTrigger("Bloom");
            state = State.SMILE;
            return 4;
        }
        else
        {
            return 0.1f;
        }
    }
}
