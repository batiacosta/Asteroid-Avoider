using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _movemenDirection;
    
    [SerializeField] private Rigidbody _playerRigidBody = null;
    [SerializeField] private float _forceMagnitude;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void RotateToFaceVelocity()
    {
        if (_playerRigidBody.velocity == Vector3.zero) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(_playerRigidBody.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector3 playerViewportPosition = _mainCamera.WorldToViewportPoint(transform.position);

        if (playerViewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        } 
        else if(playerViewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        
        if (playerViewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if(playerViewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }
        
        transform.position = newPosition;
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchWorldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

            _movemenDirection = transform.position - touchWorldPosition;
            _movemenDirection.z = 0f;
            _movemenDirection.Normalize();
        }
        else
        {
            _movemenDirection = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (_movemenDirection == Vector3.zero) return;
        
        _playerRigidBody.AddForce(_movemenDirection * _forceMagnitude, ForceMode.Force);
        _playerRigidBody.velocity = Vector3.ClampMagnitude(_playerRigidBody.velocity, _maxVelocity);
    }
}
