using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 10f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYewFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrust;

    void OnEnable() 
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    void Update()
    {
        PlayerMovement();
        PlayerRotation();
    }

    void PlayerRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrust * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYewFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    void PlayerMovement()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrust = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * moveSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrust * moveSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampYPos, transform.localPosition.z);
    }
}
