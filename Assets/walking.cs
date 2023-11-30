using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 0.01f;

    // Animator animator;

    // private void Awake() {
    //     animator = GetComponent<Animator>();

    // }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }


    private void Move() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(moveSpeed * horizontal * Time.deltaTime, moveSpeed * vertical * Time.deltaTime, 0);


        bool fire1 = Input.GetButtonDown("Fire1");
        if (fire1) {
            Debug.Log("Fire? " + fire1);
        }
        
        // animator.SetBool("IsWalkingUp", vertical > 0);
        // animator.SetBool("IsWalkingDown", vertical < 0);


    }
}
