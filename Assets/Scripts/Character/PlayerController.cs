using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private LayerMask solidObjectsLayer;
    [SerializeField]
    private LayerMask grassLayer;
    [SerializeField]
    private LayerMask interactuableLayer;

    public event Action onEncountered;

    private bool isMoving;
    private Vector2 input;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    public void HandleUpdate()
    {
        if (!isMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //No diagonal movement
            if(input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                animator.SetFloat("moveXAnim", input.x);
                animator.SetFloat("moveYAnim", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                StartCoroutine(Movement(targetPos));
            }
        }
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log("Presionado Z");
            Interact();
        }
            
    }

    private void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveXAnim"),animator.GetFloat("moveYAnim"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position,interactPos,Color.red,0.5f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactuableLayer);

        Debug.Log("rESPUESTA: "+collider);

        if(collider != null)
            collider.GetComponent<Interactuable>()?.Interact();
    }

    IEnumerator Movement(Vector3 targetPos) {
        
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos) { 
        if(Physics2D.OverlapCircle(targetPos,0.2f,solidObjectsLayer | interactuableLayer) != null)
            return false;

        return true;
    }
    
    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if(UnityEngine.Random.Range(1,101) <=10)
            {
                Debug.Log("Encountered Wild Pokemon");
                animator.SetBool("isMoving", false);
                onEncountered();
            }
        }
            
    }
}
