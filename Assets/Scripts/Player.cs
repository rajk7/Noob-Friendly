using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveH;
    private float verticalVelocity;
    private float speed = 5.0f;
    private float gravity = 30.0f;
    private float jumpForce = 10.0f;
    private bool secandJump = false;

    private Vector3 moveVector;
    private Vector3 lastMotion;
    private CharacterController controller;
    
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    private void Update()
    {
        //IsControllerGrounded();
        moveVector = Vector3.zero;
        moveH = Input.GetAxis("Horizontal") * speed;
       
       if(IsControllerGrounded())
       {
           verticalVelocity = 0;

           if(Input.GetKeyDown(KeyCode.Space))
           {
                verticalVelocity = 10;
                secandJump = true;
           }
           moveVector.x = moveH; //player can't jump in the air
       }
       else
       {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(secandJump)
                {
                    verticalVelocity = 10;
                    secandJump = false;
                }
                
            }

           verticalVelocity -= gravity * Time.deltaTime;
           moveVector.x = lastMotion.x;
       }
       //moveVector.x = moveH;
       moveVector.y = verticalVelocity;
        //moveVector = new Vector3(moveH, verticalVelocity , 0);
       controller.Move(moveVector * Time.deltaTime );
       lastMotion = moveVector;
    }
    private bool IsControllerGrounded()
    {
        Vector3 leftRayStart;
        Vector3 rightRayStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;

        leftRayStart.x -= controller.bounds.extents.x;//extents gives side of object
        rightRayStart.x += controller.bounds.extents.y;

        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down, Color.green);

        if(Physics.Raycast(leftRayStart, Vector3.down, (controller.height/2) + .2f))
        return true;

        if(Physics.Raycast(rightRayStart, Vector3.down, (controller.height/2) + .2f))
        return true;

        return false;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(controller.collisionFlags == CollisionFlags.Sides)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 2.0f);
                moveVector = hit.normal * speed;
                verticalVelocity = jumpForce;
                secandJump = true;
            }
            
        }
        switch(hit.gameObject.tag)
        {
            case "Coin":
                Destroy(hit.gameObject);
                LevelManages.Instance.CollectCoin();
                
                
                break;
            case "JumpPad":
                verticalVelocity = jumpForce * 2;
                break;
            case "Teleport":
                transform.position = hit.transform.GetChild(0).position;
                break;
            case "WinTag":
                LevelManages.Instance.Win () ;
                break;
            default:
                break;
        }

    }
}
