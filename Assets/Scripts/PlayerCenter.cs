using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCenter : MonoBehaviour

    //todo workout why sometimes slow on first play of scene

{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 15f;
    [Tooltip("In m")] [SerializeField] float yRange = 10f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position based/parameters")]
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = 1.2f;

    [Header("Control-throw based/parameters")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -30f;


    float xThrow, yThrow;

    bool isControlEnabled = true;


    // Start is called before the first frame update
    //deleted for lecture called SendMessage() Between Components
    // void Start()
    // {

    // }
    //good to help debug
    // void OnCollisionEnter(Collision collision)
    // {
    //     print("Player collided with something.");
    // }
    //good to help debug
    // void OnTriggerEnter(Collider other)
    // {
    //     print("Player triggered something.");
    // }

    // Update is called once per frame
    void Update()
    {
        // print("Update");
        if (isControlEnabled)
        {
            // print("ready");
            ProcessTranslations();
            ProcessRotation();
            ProcessFiring();
        }

    }

    void OnPlayerDeath() //called by string reference
    {
        // print("Controls frozen");
        isControlEnabled = false;
        // Destroy(gameObject);
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

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;


        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        //print("clampedXPos");
        //print(clampedXPos);
        //print("clampedYPos");
        //print(clampedYPos);

        //localPosition needs a vector three it is not included

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else{
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
