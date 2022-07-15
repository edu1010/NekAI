using UnityEngine;

public class Volatile : MonoBehaviour
{
    [Tooltip("Seconds to auto disable.")] [SerializeField]
    private float lifeSpan;

    public float LifeSpan => lifeSpan;

    public void OnEnable()
    {
        Invoke(nameof(Disable), lifeSpan);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Disable));
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}