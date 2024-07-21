using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : Boat
{
    public enum Player
    {
        Player1 = 0,
        Player2 = 1,
        None = 2,
    }

    [SerializeField] protected Collider2D _collider;
    private bool justHit;
    private float hitElapsedTime;

    [Header("Controls")]
    [SerializeField] private Player player;
    [SerializeField] private PlayerInput playerInput;
    private InputAction movementAction;
    private InputAction passWeaponAction;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector2 inputVector;

    [Header("Weapon")]
    [SerializeField] private SpriteRenderer _weapon;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootSpeed;

    private bool _owningWeapon = false;
    private float _timerForNextBullet = 0.0f;

    public InputAction PassWeaponAction { get { return passWeaponAction; } }
    public Player PlayerId { get { return player; } }
    public bool OwningWeapon => _owningWeapon;
    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        playerInput.Enable();
        movementAction = player == Player.Player1 ? playerInput.Player1.Movement : playerInput.Player2.Movement;
        passWeaponAction = player == Player.Player1 ? playerInput.Player1.PassWeapon : playerInput.Player2.PassWeapon;

        movementAction.performed += MovementAction_performed;
        movementAction.canceled += MovementAction_canceled;

        ChangableWeapon.OnOwnershipChanged.AddListener((Player playerReceivingWeapon) => { if (player == playerReceivingWeapon) { ReceiveWeapon(); } });
        ChangableWeapon.OnSendingWeapon.AddListener(() => { SendWeapon(); });

        _weapon.enabled = false;
    }

    private void MovementAction_canceled(InputAction.CallbackContext context)
    {
        inputVector = Vector2.zero;
    }

    private void MovementAction_performed(InputAction.CallbackContext obj)
    {

        inputVector = obj.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.AddForce(inputVector * speed * Time.deltaTime, ForceMode2D.Force);
        if (justHit)
        {
            hitElapsedTime += Time.deltaTime;

            float alpha = Mathf.PingPong(Time.deltaTime * 30, 0.9f) + 0.1f;
            visuals.SetAlpha(alpha);
            _collider.enabled = false;

            if (hitElapsedTime >= 1f)
            {
                if (health <= 0)
                {
                    GameManager.OnGameOver.Invoke();
                    visuals.SetAlpha(0);
                }
                else
                {

                    _collider.enabled = true;
                    visuals.SetAlpha(1);
                }
                justHit = false;
                hitElapsedTime = 0f;
            }
            return;
        }

        if (_owningWeapon)
        {
            _timerForNextBullet += Time.deltaTime;
            if (_timerForNextBullet >= 1 / _shootSpeed)
            {
                Shoot();
            }
        }
    }

    private void ReceiveWeapon()
    {
        ChangeWaponState(true);
    }

    private void SendWeapon()
    {
        ChangeWaponState(false);
    }

    private void ChangeWaponState(bool active)
    {
        _owningWeapon = active;
        _weapon.enabled = active;
    }

    public override void GetDamaged()
    {
        base.GetDamaged();
        justHit = true;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position+Vector3.up, Quaternion.identity);
        bullet.Shoot(Vector3.up);

        _timerForNextBullet = 0;
    }
}
