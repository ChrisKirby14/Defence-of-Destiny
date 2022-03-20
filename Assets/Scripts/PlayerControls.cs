using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [Tooltip("How fast ship moves up and down based on players input")] 
    [SerializeField] float moveSpeed = 30f;
    [Tooltip("How far ship can move on the X axis")] [SerializeField] float xRange = 10f;
    [Tooltip("How far ship can move on the Y axis")] [SerializeField] float yRange = 10f;
   
    [Header("Laser gun array")]
    [Tooltip("Place your firing gameobjects in here")] [SerializeField] GameObject[] lasers;
   
    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYewFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrust;

    private ParticleSystem ps;

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
        ProcessFiring();
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

    void ProcessFiring()
    {
       if(fire.ReadValue<float>() > 0.5)
       {
           SetLasersActive(true);
       }

       else
       {
           SetLasersActive(false);
       }
    }

    void SetLasersActive(bool isActive)
    {

        //for each of the lasers that we have, turn them on (activate them)
        foreach(GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
