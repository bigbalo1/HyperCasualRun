using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider mySlider;
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; //0: left 1:middle 2:right
    public float laneDistance = 4;//the distance between two lane

     public bool isGrounded = false ;
     /*public LayerMask groundLayer;
     public Transform groundCheck;
     */
    public float jumpForce;
    public float Gravity = -20;

    
    public Animator animator;
    private bool isSliding = false;

    public float sliderValue ;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        mySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
     void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        //Increase speed
        if(forwardSpeed  < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime ;
        

        //sliderValue = forwardSpeed;


        animator.SetBool("IsGameStarted", true);
        direction.z = forwardSpeed * sliderValue;
        
        //isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("IsGrounded", false);
        if (controller.isGrounded)
        {
           
            if (SwiptManager.swipeUp )
            {
                animator.SetBool("IsGrounded", true);
                Jump();
              //  FindObjectOfType<AudioManager>().PlaySound("Jump");
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

        if (SwiptManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
            FindObjectOfType<AudioManager>().PlaySound("Slide");
        }

        //gather the input on which lane we should be

        if (SwiptManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
            FindObjectOfType<AudioManager>().PlaySound("Move");
        }
        if (SwiptManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
            FindObjectOfType<AudioManager>().PlaySound("Move");
        }

        //calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
    
        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        //this can be use to solve the problem off colliding 
        /*transform.position = Vector3.Lerp(transform.position, targetPosition, 70 * Time.fixedDeltaTime );
        controller.center = controller.center; */
        //transform.position = targetPosition;
        //or the one below
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
            
      

       //move player
        controller.Move(direction * Time.deltaTime);
    }

    
   
    private void Jump()
    {
        direction.y = jumpForce;
        FindObjectOfType<AudioManager>().PlaySound("Jump");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {

        isSliding = true;
        animator.SetBool("IsSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("IsSliding", false);
        isSliding = false;
    }
    public void ValueChangeCheck()
    {
        Debug.Log(mySlider.value);

        sliderValue = mySlider.value;
    }
}
