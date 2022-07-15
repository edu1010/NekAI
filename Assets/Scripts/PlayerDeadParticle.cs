using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDeadParticle : ExtendedMonoBehaviour
{
    [SerializeField] private Range<float> randomSpeed, randomTime, randomScale;

    private Rigidbody2D _rigidbody2D;
    private float _finalTime;
    private Vector2 _currentVelocity;
    private Vector3 _currentScale;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _finalTime = Random.Range(randomTime.min, randomTime.max);
        _rigidbody2D.velocity = transform.right * Random.Range(randomSpeed.min, randomSpeed.max);
        transform.localScale = Vector3.one * Random.Range(randomScale.min, randomScale.max);
        InvokeLater(() => gameObject.SetActive(false), _finalTime);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, Vector2.zero, ref _currentVelocity, _finalTime);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.zero, ref _currentScale, _finalTime);
    }
}
