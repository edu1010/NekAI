using UnityEngine;

[RequireComponent(typeof(Collectible))]
public class BlasterCollectible : MonoBehaviour
{
   // [SerializeField] private GameObject text;

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
        FindObjectOfType<PlayerController>().playerFlags.canShoot = true;
        gameObject.SetActive(false);
       // text?.SetActive(true);
    }
}