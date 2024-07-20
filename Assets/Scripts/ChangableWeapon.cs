using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ChangableWeapon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _visuals;
    [SerializeField] private WeaponConection _weaponContection;
    [SerializeField] private float _speed;

    private bool _moving;
    private BoatController _boatSending;
    private BoatController _boatReceivig;
    private float _lerpValue = 0.0f;
    public static UnityEvent OnSendingWeapon = new UnityEvent();
    public static UnityEvent<BoatController.Player> OnOwnershipChanged = new UnityEvent<BoatController.Player>();

    void Start()
    {
        if (_weaponContection == null)
        {
            _weaponContection = GetComponentInParent<WeaponConection>();
        }
        _weaponContection.BoatsConected[0].PassWeaponAction.performed += (InputAction.CallbackContext context)
            =>
        { ChangeOwnership(_weaponContection.BoatsConected[0].PlayerId); };

        _weaponContection.BoatsConected[1].PassWeaponAction.performed += (InputAction.CallbackContext context)
            =>
        { ChangeOwnership(_weaponContection.BoatsConected[1].PlayerId); };

        OnOwnershipChanged?.Invoke(BoatController.Player.Player1);

    }


    void Update()
    {
        if (_moving)
        {
            _lerpValue += _speed * (_speed / Vector3.Magnitude(_boatSending.transform.position - _boatReceivig.transform.position)) * Time.deltaTime;
            if (_lerpValue < 1)
            {
                _visuals.transform.position = Vector3.Lerp(_boatSending.transform.position, _boatReceivig.transform.position, _lerpValue);
            }
            else
            {
                _moving = false;
                _visuals.enabled = false;
                OnOwnershipChanged?.Invoke(_boatReceivig.PlayerId);
            }
            return;
        }

        Shoot();
    }

    private void ChangeOwnership(BoatController.Player playerSendingWeapon)
    {
        if (!_moving)
        {
            _boatSending = _weaponContection.BoatsConected.Find(x => x.PlayerId == playerSendingWeapon);
            if (_boatSending.OwningWeapon)
            {
                _lerpValue = 0.0f;
                _boatReceivig = _weaponContection.BoatsConected.Find(x => x.PlayerId != playerSendingWeapon);
                _visuals.transform.position = _boatSending.transform.position;
                _visuals.enabled = true;
                _moving = true;
                OnSendingWeapon?.Invoke();
            }
        }
    }

    private void Shoot()
    {

    }
}