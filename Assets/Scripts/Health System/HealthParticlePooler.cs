using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class HealthParticlePooler : MonoBehaviour
{
    [SerializeField] private float minParticles, maxParticles;
    [SerializeField] private BasicDamageTaker basicDamageTaker;

    private ObjectPooler _objectPooler;

    private void Awake()
    {
        _objectPooler = GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        basicDamageTaker.OnDie += OnDie;
    }

    private void OnDie(BasicDamageTaker obj)
    {
        var amount = Random.Range(minParticles, maxParticles);
        for (var i = 0; i < amount; i++)
        {
            var particle = _objectPooler.GetFromPool();
            particle.transform.position = transform.position;
            particle.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
            particle.SetActive(true);
        }
    }
}
