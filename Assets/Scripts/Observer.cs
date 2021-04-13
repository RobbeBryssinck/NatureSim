using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public float AccelerationMod = 0.03f;
    public float XAxisSensitivity = 2.0f;
    public float YAxisSensitivity = 2.0f;
    public float DecelerationMod = 0.03f;

    [Range(0, 89)] public float MaxXAngle = 89.0f;

    public float MovementSpeedLimit = 0.04f;
    public float MaxMovementSpeed = 0.1f;
    public float MinMovementSpeed = 0.02f;
    public float DeltaSpeedLimit = 0.01f;

    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Up = KeyCode.Q;
    public KeyCode Down = KeyCode.E;
    public KeyCode IncSpeed = KeyCode.LeftShift;
    public KeyCode DecSpeed = KeyCode.Space;

    private Vector3 _moveSpeed;
    private float _rotationX;

    void Start()
    {
        _moveSpeed = Vector3.zero;
    }

    void Update()
    {
        HandleMouseRotation();

        var acceleration = HandleKeyInput();

        _moveSpeed += acceleration;

        HandleDeceleration(acceleration);

        HandleLimitChange();

        transform.Translate(_moveSpeed);
    }

    private Vector3 HandleKeyInput()
    {
        Vector3 acceleration = Vector3.zero;

        if (Input.GetKey(Forwards))
        {
            acceleration.z += 1;
        }

        if (Input.GetKey(Backwards))
        {
            acceleration.z -= 1;
        }

        if (Input.GetKey(Left))
        {
            acceleration.x -= 1;
        }

        if (Input.GetKey(Right))
        {
            acceleration.x += 1;
        }

        if (Input.GetKey(Up))
        {
            acceleration.y += 1;
        }

        if (Input.GetKey(Down))
        {
            acceleration.y -= 1;
        }

        return acceleration.normalized * AccelerationMod;
    }

    private void HandleMouseRotation()
    {
        var rotationHorizontal = XAxisSensitivity * Input.GetAxis("Mouse X");
        var rotationVertical = YAxisSensitivity * Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * rotationHorizontal, Space.World);

        var rotationY = transform.localEulerAngles.y;

        _rotationX += rotationVertical;
        _rotationX = Mathf.Clamp(_rotationX, -MaxXAngle, MaxXAngle);

        transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
    }

    private void HandleDeceleration(Vector3 acceleration)
    {
        if (Mathf.Approximately(Mathf.Abs(acceleration.x), 0))
        {
            if (Mathf.Abs(_moveSpeed.x) < DecelerationMod)
                _moveSpeed.x = 0;
            else
                _moveSpeed.x -= DecelerationMod * Mathf.Sign(_moveSpeed.x);
        }

        if (Mathf.Approximately(Mathf.Abs(acceleration.y), 0))
        {
            if (Mathf.Abs(_moveSpeed.y) < DecelerationMod)
                _moveSpeed.y = 0;
            else
                _moveSpeed.y -= DecelerationMod * Mathf.Sign(_moveSpeed.y);
        }

        if (Mathf.Approximately(Mathf.Abs(acceleration.z), 0))
        {
            if (Mathf.Abs(_moveSpeed.z) < DecelerationMod)
                _moveSpeed.z = 0;
            else
                _moveSpeed.z -= DecelerationMod * Mathf.Sign(_moveSpeed.z);
        }
    }

    private void HandleLimitChange()
    {
        if (Input.GetKeyDown(IncSpeed) && MovementSpeedLimit < MaxMovementSpeed)
            MovementSpeedLimit += DeltaSpeedLimit;

        if (Input.GetKeyDown(DecSpeed) && MovementSpeedLimit > MinMovementSpeed)
            MovementSpeedLimit -= DeltaSpeedLimit;

        if (_moveSpeed.magnitude > MovementSpeedLimit)
            _moveSpeed = _moveSpeed.normalized * MovementSpeedLimit;
    }
}
