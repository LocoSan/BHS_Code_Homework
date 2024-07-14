using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _horizontalMovementMultiplier;
    [SerializeField, Range(0, 1)] private float _verticalMovementMultiplier;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;

    private Vector3 _targetPosition => _target.position;
    private Vector3 _lastTargetPosition;
    private Vector3 _cameraPosition;
    private float _textureWidth;

    private void Start()
    {
        _lastTargetPosition = _targetPosition;
        _cameraPosition = _camera.transform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        _textureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void Update()
    {
        Vector3 delta = _targetPosition - _lastTargetPosition;
        delta *= new Vector2(_horizontalMovementMultiplier, _verticalMovementMultiplier);
        transform.position += delta;
        _lastTargetPosition = _targetPosition;
        _cameraPosition = _camera.transform.position;
        if (Mathf.Abs(_cameraPosition.x - transform.position.x) >= _textureWidth)
        {
            float offsetX = (_cameraPosition.x - transform.position.x) % _textureWidth;
            transform.position = new Vector3(_cameraPosition.x + offsetX, transform.position.y);
        }
    }
}