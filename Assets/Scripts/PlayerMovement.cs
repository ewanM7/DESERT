using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;

    public float MoveSpeed;
    public float TurnRate;

    public Vector2 MovementInput;
    public float VerticalMovement;
    public Vector3 CameraForward;
    public Vector3 CameraRight;

    public const float Gravity = -30f;
    public const float TerminalVelocity = -30f;

    private float tmpVel;

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
            VerticalMovement = -3f;
        }
        else
        {
            VerticalMovement += Gravity * Time.deltaTime;
            VerticalMovement = Mathf.Clamp(VerticalMovement, TerminalVelocity, -TerminalVelocity);
        }

        CameraForward = GameManager.Instance._CameraManager.MainCamera.transform.forward;
        CameraRight = GameManager.Instance._CameraManager.MainCamera.transform.right;

        CameraForward.y = 0;
        CameraForward.Normalize();
        CameraRight.y = 0;
        CameraRight.Normalize();

        Vector3 FinalMovement = ((CameraRight * MovementInput.x) + (CameraForward * MovementInput.y)).normalized * MoveSpeed;

        targetPlayerForward = new Vector3(FinalMovement.x, 0, FinalMovement.z);
        
        if (targetPlayerForward != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, targetPlayerForward, TurnRate * Time.deltaTime);
        }

        Controller.Move(new Vector3(FinalMovement.x, VerticalMovement, FinalMovement.z) * Time.deltaTime);
    }
}
