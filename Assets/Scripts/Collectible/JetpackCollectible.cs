using UnityEngine;

[RequireComponent(typeof(Collectible))]
public class JetpackCollectible : MonoBehaviour
{
    //[SerializeField] private GameObject triggerText;

    private Collectible _collectible;

    private void Awake()
    {
        _collectible = GetComponent<Collectible>();
    }

    private void OnEnable()
    {
        _collectible.OnCollectedEvent += ActivateFlying;
    }

    private void OnDisable()
    {
        _collectible.OnCollectedEvent -= ActivateFlying;
    }

    private void ActivateFlying()
    {
        FindObjectOfType<PlayerController>().playerFlags.canFly = true;
        //triggerText.SetActive(true);
    }
}