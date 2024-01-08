using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float sensitivity = 2.0f; // Mouse sensitivity for looking
    public float jumpForce = 8.0f;
    public float gravity = 9.8f;
    private float verticalVelocity = 0.0f;
    private bool isJumping = false;

    public float normalMoveSpeed;
    private CharacterController controller;
    private Animator animator;

    private bool isCrouched = false;
    private bool hasPistol = false;
    private bool isSprinting = false;
    //stamina
  
    public float sprintCost;
    public float jumpCost;

     public float maxStamina;
    public float stam;

    public float chargeRate;
    public Player player;
    


    private Coroutine recharge;

    



    public Stambar stambar;

    private float rotationX = 0.0f; // Rotation around the X-axis for looking

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // Assuming the Animator component is on the same GameObject as the script.
        normalMoveSpeed = moveSpeed; // Store the normal movement speed.
        maxStamina = player.stamina;
        stam=maxStamina;
        stambar.SetStam(maxStamina, maxStamina);
        
    }

    void Update()
    {
        
            // Mouse Input for Looking
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); // Limit vertical rotation

            // Apply rotation to the player
            transform.Rotate(0, mouseX, 0);
            Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);



            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);


            // Apply gravity continuously
            if (controller.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime; // Reset vertical velocity when grounded.
                if (Input.GetButtonDown("Jump") && stam > 0)
                {
                    verticalVelocity = jumpForce;
                    isJumping = true;
                    stam -= jumpCost;
                    if (stam < 0) stam = 0;
                    stambar.UpdateStamBar(stam, maxStamina);

                    if (recharge != null) StopCoroutine(recharge);
                    recharge = StartCoroutine(RechargeStam());

                }
            }

            else
            {
                verticalVelocity -= gravity * Time.deltaTime; // Apply gravity when not grounded.
            }

            moveDirection = new Vector3(horizontalInput, verticalVelocity, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);


            // Check if the "Shift" key is held down to sprint.
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && stam > 0)
            {
                isSprinting = true;
                animator.SetBool("isSprinting", true);
                moveSpeed = normalMoveSpeed * 1.5f; // increase the movement speed.
                stam -= sprintCost * Time.deltaTime;
                if (stam < 0) stam = 0;
                stambar.UpdateStamBar(stam, maxStamina);
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStam());
            }
            else
            {
                isSprinting = false;
                animator.SetBool("isSprinting", false);
                moveSpeed = normalMoveSpeed; // Reset the movement speed to normal.
            }

            moveDirection *= moveSpeed;

            controller.Move(moveDirection * Time.deltaTime);

            // Check if the player is moving and set the "isMoving" parameter in the animator.
            if (moveDirection.magnitude > 0.8f)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            
        
        
    }

    public IEnumerator RechargeStam(){
        yield return new WaitForSeconds(1f);

        while (stam < maxStamina)
        {
            stam += chargeRate/10f;
            if(stam>maxStamina)stam=maxStamina;
            stambar.UpdateStamBar(stam,maxStamina);
            yield return new WaitForSeconds(1f);
        }
        
    }
}
