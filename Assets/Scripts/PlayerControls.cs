using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] InputAction movement;
    [Header("Fire")]
    [SerializeField] InputAction fire;
    [Header("General Play Setup")]
    [SerializeField] float movementSpeed;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yMin = -10f;
    [SerializeField] float yMax = 10f;

    [Header("Ship lasers")]
    [Tooltip("Add ship's laser game objects in here")]
    [SerializeField] GameObject[] lasers;

    [Header("Rotation Variables")]
    [Tooltip("Affects how much ship rotates up and down based on its local position")] 
    [SerializeField] float pitchFactor = 5f;
    [Tooltip("Affects how much ship rotates up and down based on vertical movement input")] 
    [SerializeField] float controlFactor = 10f;
    [Tooltip("Affects how much ship rotates left and right based on its local position")] 
    [SerializeField] float yawFactor = 5f;
    [Tooltip("Affects how much ship rotates around its forward axis based on horizontal movement input")] 
    [SerializeField] float rollFactor = 5f;

    Vector2 currentInputVector;
    Vector2 smoothInputVelocity;
    [Tooltip("Affects input dampening")]
    [SerializeField] float smoothInputSpeed;

    public bool disableControls;

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (!disableControls)
        {
            Translate();
            Rotate();
            FireLasers(true);
        }
        else FireLasers(false);
    }

    private void Translate()
    {
        Vector2 input = movement.ReadValue<Vector2>();

        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);

        float clampedHorizontalMovement = Mathf.Clamp(transform.localPosition.x, -xRange, xRange);
        float clampedVerticalMovement = Mathf.Clamp(transform.localPosition.y, yMin, yMax);

        transform.localPosition = new Vector3
            (clampedHorizontalMovement + currentInputVector.x * movementSpeed * Time.deltaTime,
            clampedVerticalMovement + currentInputVector.y * movementSpeed * Time.deltaTime,
            transform.localPosition.z);
    }

    private void Rotate()
    {
        float pitch = -(transform.localPosition.y * pitchFactor + currentInputVector.y * controlFactor);
        float yaw = transform.localPosition.x * yawFactor;
        float roll = -(currentInputVector.x * rollFactor);
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void FireLasers(bool enableEmission)
    {
        // bugfix: Prevents laser still firing even after collision 
        if (!enableEmission)
        {
            foreach (GameObject o in lasers)
            {
                var emission = o.GetComponent<ParticleSystem>().emission;
                emission.enabled = false;
            }
            return;
        }

        if(fire.ReadValue<float>() > 0)
        {
            foreach(GameObject o in lasers)
            {
                var emission = o.GetComponent<ParticleSystem>().emission;
                emission.enabled = true;
            }
        }
        else
        {
            foreach(GameObject o in lasers)
            {
                var emission = o.GetComponent<ParticleSystem>().emission;
                emission.enabled = false;
            }
        }
    }
}
