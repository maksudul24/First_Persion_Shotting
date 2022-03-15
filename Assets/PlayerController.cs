﻿using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;
    
    void Start()
    {   
        motor = GetComponent<PlayerMotor>();

    }

    void Update()
    {
        //calculating movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        
        

        Vector3 _moveHorizontal = transform.right * _xMov;
        Vector3 _moveVertical = transform.forward * _zMov;

        Vector3 _velocity  = (_moveHorizontal + _moveVertical).normalized * speed;
        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation =new Vector3(0f,_yRot,0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //camera rotaion 
        float _xRot = Input.GetAxisRaw("Mouse Y");
 
        Vector3 _cameraRotation =new Vector3(_xRot,0f,0f) * lookSensitivity;

        motor.CameraRotate(_cameraRotation);


    }
}
