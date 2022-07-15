using UnityEngine;

public class SingleAttackController : MonoBehaviour
{
    private GameObject obj;

    private void Start()
    {
        obj = transform.GetChild(0)?.gameObject;
        if (!obj.GetComponent<Attack>()) obj = null;
        obj?.SetActive(false);
    }

    public void Attack()
    {
        obj?.SetActive(true);
    }
}