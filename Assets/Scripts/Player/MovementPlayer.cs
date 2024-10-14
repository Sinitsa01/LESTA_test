using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private Transform mainCameraTransform;
    public Vector3 currentTargetRotation;
    private float timeToReachTargetRotation = 0.14f;
    private Vector3 dampedTargetRotationCarrentVelocity;
    private float dampedTargetRotationPassedTime;
    [SerializeField] LayerMask layerMask;
    private float maxDistance = 0.1f;


    public void GroundCheck()
    {

        player.SetIsGrounded(Physics.Raycast(player.transform.position, Vector3.down, maxDistance, layerMask));

    }

    public void Move()
    {
        Vector2 playerMovementInput = player.PlayerMovementInput;
        if (playerMovementInput == Vector2.zero)
        {
            return;
        }

        Vector3 movementDirection = new Vector3(playerMovementInput.x, 0f, playerMovementInput.y);

        float targetRotationYAngle = Rotate(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        Vector3 currentPlayerHorizontalVelosity = playerBody.velocity;

        currentPlayerHorizontalVelosity.y = 0f;

        
        playerBody.MovePosition(playerBody.position + targetRotationDirection * player.BaseSpeed * Time.deltaTime);

    }

    private Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    private float Rotate(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        directionAngle += mainCameraTransform.eulerAngles.y;

        if (directionAngle > 360f)
        {
            directionAngle -= 360f;
        }

        if (directionAngle != currentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        RotateTowardsTargetRotation();

        return directionAngle;

    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        currentTargetRotation.y = targetAngle;

        dampedTargetRotationPassedTime = 0f;
    }

    public void Jump()
    {
        if (player.IsJump && player.IsGrounded)
        {
            Vector3 jumpForseV = new Vector3(0f, player.JumpForse, 0f);
            playerBody.AddForce(jumpForseV, ForceMode.VelocityChange);

        }
    }

    private void RotateTowardsTargetRotation()
    {
        float currentYAngle = playerBody.rotation.eulerAngles.y;

        if (currentYAngle == currentTargetRotation.y)
        {
            return;
        }

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y, ref dampedTargetRotationCarrentVelocity.y, timeToReachTargetRotation - dampedTargetRotationPassedTime);

        dampedTargetRotationPassedTime += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

        playerBody.MoveRotation(targetRotation);

    }

    
}
