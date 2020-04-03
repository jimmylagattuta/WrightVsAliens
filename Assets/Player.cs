using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 15f;
    [Tooltip("In m")] [SerializeField] float yRange = 10f;

    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 1f;
    [SerializeField] float controlRollFactor = -15f;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslations();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        //        trasform.localRotation.x < dont do like this, create a rotation
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslations()
    {
        //this will also work for a game remote
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;


        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        print("clampedXPos");
        print(clampedXPos);
        print("clampedYPos");
        print(clampedYPos);

        //localPosition needs a vector three it is not included

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
