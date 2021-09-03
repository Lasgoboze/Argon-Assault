using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    float xThrow;
    float yThrow;
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input.")]
     [SerializeField] float controlSpeed = 30f;
     [Tooltip("How fast your player moves Horizontally.")]
    [SerializeField] float xRange = 16f;
    [Tooltip("How fast your player moves Vertically.")]
    [SerializeField] float yRange = 10f;
    [Header("Laser Gun Array")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2.5f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -14f;
    [SerializeField] float controlRollFactor = -15f;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);

    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        float newYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring() 
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else 
        {
            SetLasersActive(false);
        }

        void SetLasersActive(bool isActive)
        {
            foreach(GameObject laser in lasers)
            {
                var emissionModule = laser.GetComponent<ParticleSystem>().emission;
                emissionModule.enabled = isActive;
            }
        }
    }
}