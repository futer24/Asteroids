using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : InfiniteWorldBehaviour, IDestroyable
{
    private GameControls playerControls;
    [HideInInspector] public InputAction fire;

    private Vector2 movement = default;

    [SerializeField] private GameEventVector3 _playerDiePositionGameEvent = default;
    [SerializeField] private GameEvent _playerDieGameEvent = default;
    [SerializeField] private GameEventVector3 _playerShootGameEvent = default;

    public GameObject engineTrails;
    public GameObject shield;

    private IWeapon currentWeapon;
 
    private bool isShooting = false;

    public float rotationSpeed = 200f;
    public float movementSpeed = 150f;

    public bool isInmune = true;

    public override void Awake()
    {
        base.Awake();
        playerControls = new GameControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Shoot.started += OnShootStart;
        playerControls.Player.Shoot.canceled += OnShootCancel;

    }

    private void OnDisable()
    {
        playerControls.Player.Shoot.started -= OnShootStart;
        playerControls.Player.Shoot.canceled -= OnShootCancel;
        playerControls.Disable();
    }

    private void ToggleShield(bool enable)
    {
        isInmune = enable;
        shield.SetActive(enable);
    }

    IEnumerator Start()
    {
        SetCurrentWeapon(new LaserWeapon());
        ToggleShield(true);
        yield return new WaitForSeconds(2f);
        ToggleShield(false);
       
    }

    public void SetCurrentWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
    }

    // Update is called once per frame
    private void Update()
    {
         movement = playerControls.Player.Move.ReadValue<Vector2>();
      
         ProcessMovementRotate();
         if(isShooting) ProcessShoot();
         CheckEngineTrailActive(movement.y > 0f);
    }

    private void FixedUpdate()
    {
        ProcessMovementThrust();
    }

    private void ProcessMovementThrust()
    {
        if (movement.y > 0f)
            rBody.AddForce(-transform.up * movementSpeed * Time.deltaTime);
    }

    private void ProcessMovementRotate()
    {
        if (movement.x != 0f)
            transform.Rotate(Mathf.Sign(movement.x) * Vector3.forward * rotationSpeed * Time.deltaTime);    //rBody.AddTorque(3f * Mathf.Sign(movement.x) * transform.right);
    }

    private void CheckEngineTrailActive(bool enable)
    {
        if (enable && engineTrails.activeSelf)
            return;
        else
            engineTrails.SetActive(enable);
    }

    private void ProcessShoot()
    {
        bool canShoot = currentWeapon.Shoot(transform.TransformPoint(Vector3.down * 5), -transform.up);
        if(canShoot) _playerShootGameEvent.RaiseEvent(transform.position);
     
    }

    public void OnShootStart(InputAction.CallbackContext context)  
    {
        isShooting = true;
    }

    public void OnShootCancel(InputAction.CallbackContext context)
    {
        isShooting = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(!isInmune)
            Destroy();
    }

    public void Destroy()
    {
        rBody.velocity = Vector3.zero;
        _playerDiePositionGameEvent.RaiseEvent(transform.position);
        _playerDieGameEvent.RaiseEvent();
        Destroy(gameObject, 0.3f);


    }
}