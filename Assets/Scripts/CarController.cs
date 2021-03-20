using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
   

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    public Text changingText;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField]  public Transform Steering;


    public void UpdateSpeed()
    {
        float speed = GetComponent<Rigidbody>().velocity.magnitude*5f;
        int rounded = (int)Math.Round(speed, 0);
        changingText.text = rounded.ToString();
    }


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateSpeed();
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {

        currentSteerAngle = maxSteerAngle * horizontalInput;
        //var rotation = Vector3.up * 0; ;

        var rotation = Vector3.up * 0;
        Steering.Rotate(rotation, Space.Self);
        rotation.y = currentSteerAngle;

        Steering.Rotate(rotation, Space.Self);
        //Steering.rotation = Quaternion.AngleAxis(currentSteerAngle, Vector3.up);
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }


    float maxTurnAngle = 80;

    void Update()
    {

        float turnAngle = this.transform.eulerAngles.y + Input.GetAxisRaw("Horizontal");

        if (turnAngle > 90)
        {
            turnAngle = 90;
        }
        else if (turnAngle < -90)
        {
            turnAngle = -90;
        }


        // Steering.localRotation = Vector3.back * Mathf.Clamp((Input.GetAxis("Horizontal") * 100), -maxTurnAngle, maxTurnAngle);

        Steering.localRotation= Quaternion.Euler(Steering.rotation.x, Mathf.Clamp((Input.GetAxis("Horizontal") * 100), -maxTurnAngle, maxTurnAngle), Steering.rotation.z);

    }
}
