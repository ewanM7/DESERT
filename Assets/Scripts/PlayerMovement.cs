using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;

    public float MoveSpeed;
    public float TurnRate;

    public Vector2 InputMovement;
    public float VerticalMovement;
    public Vector3 CameraForward;
    public Vector3 CameraRight;

    public const float Gravity = -30f;
    public const float TerminalVelocity = -30f;


    private Vector3 targetPlayerForward;
    private Vector3 currentPlayerForward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.isGrounded)
        {
            VerticalMovement = -2f;
        }
        else
        {
            VerticalMovement += Gravity * Time.deltaTime;
            VerticalMovement = Mathf.Clamp(VerticalMovement, TerminalVelocity, -TerminalVelocity);
        }

        CameraForward.y = 0;
        CameraForward.Normalize();
        CameraRight.y = 0;
        CameraRight.Normalize();

        Vector3 FinalMovement = ((CameraRight * InputMovement.x) + (CameraForward * InputMovement.y)).normalized * MoveSpeed;

        targetPlayerForward = new Vector3(FinalMovement.x, 0, FinalMovement.z);
        transform.forward = Vector3.Slerp(currentPlayerForward, targetPlayerForward, TurnRate * Time.deltaTime);
        currentPlayerForward = transform.forward;

        Controller.Move(new Vector3(FinalMovement.x, VerticalMovement, FinalMovement.z) * Time.deltaTime);
    }
}
