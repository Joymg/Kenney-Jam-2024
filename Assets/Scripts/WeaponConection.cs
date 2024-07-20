using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponConection : MonoBehaviour
{
    [SerializeField] private List<BoatController> _boatsToConnect;
    [SerializeField] private SpriteRenderer _tubeSprite;

    private Vector2 _tubeSize;
    
    public List<BoatController> BoatsConected { get { return _boatsToConnect; } }

    void Start()
    {
        if (_tubeSprite == null)
        {
            _tubeSprite = GetComponentInChildren<SpriteRenderer>();
        }

        SetTubeTransform();
    }

    
    void Update()
    {
        SetTubeTransform();
    }

    private void SetTubeTransform()
    {
        _tubeSprite.transform.position = _boatsToConnect[1].transform.position;
        _tubeSprite.transform.rotation *= Quaternion.FromToRotation(_tubeSprite.transform.right, _boatsToConnect[0].transform.position - _boatsToConnect[1].transform.position);
        //_tubeSprite.transform.LookAt(_boatsToConnect[0].transform.position);
        _tubeSize = new Vector2(Vector3.Magnitude(_boatsToConnect[1].transform.position - _boatsToConnect[0].transform.position), _tubeSprite.size.y);
        _tubeSprite.size = _tubeSize;
    }
}
