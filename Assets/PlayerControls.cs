using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalThrow = movement.ReadValue<Vector2>().x;
        float verticalThrust = movement.ReadValue<Vector2>().y;


    //    float horizontalThrow = Input.GetAxis("Horizontal");
        Debug.Log (horizontalThrow);

    //    float verticalThrust = Input.GetAxis("Vertical");
        Debug.Log (verticalThrust);
    }
}
