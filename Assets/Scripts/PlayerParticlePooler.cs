using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerParticlePooler : MonoBehaviour
{
    [SerializeField] private HealthBarController healthBarController;
    [SerializeField] private PlayerHealthSystem playerHealthSystem;

    private ObjectPooler _objectPooler;

    private void Awake()
    {
        _objectPooler = GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        playerHealthSystem.OnPlayerDie += OnDie;
    }

    private void OnDisable()
    {
        playerHealthSystem.OnPlayerDie -= OnDie;
    }

    private void OnDie()
    {
        Debug.Log("Ded!");
        for (var i = 0; i < healthBarController.HealthBar; i++)
        {
            var particle = _objectPooler.GetFromPool();
            particle.transform.position = transform.position;
            particle.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
            particle.SetActive(true);
        }

        healthBarController.HealthBar = 0;
    }
}