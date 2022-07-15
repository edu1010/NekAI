using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform Camera;
    public float duration = 0.02f;
    public float magnitude = 1f;
    public float TimePauseShake = 2f;
    private bool CanShake;
    private float counter;
    private bool WasShake;

    private void Update()
    {
        if (CanShake && !WasShake) StartCoroutine(shake());
        if (WasShake)
        {
            counter += Time.deltaTime;
            if (counter > TimePauseShake)
            {
                counter = 0;
                WasShake = false;
            }
        }
    }

    private void OnEnable()
    {
        var playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        playerHealthSystem.OnPlayerDamaged += Shake;
        playerHealthSystem.OnPlayerDie += Shake;
    }

    private void OnDisable()
    {
        var playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        playerHealthSystem.OnPlayerDamaged -= Shake;
        playerHealthSystem.OnPlayerDie += Shake;
    }

    public void Shake()
    {
        CanShake = true;
    }

    public IEnumerator shake()
    {
        var originalPos = Camera.localPosition;

        var timeShaked = 0.0f;

        while (timeShaked < duration)
        {
            var x = Random.Range(-1f, 1f) * magnitude;
            var y = Random.Range(-1f, 1f) * magnitude;

            Camera.localPosition = new Vector3(Camera.position.x + x, Camera.position.y + y, originalPos.z);

            timeShaked += Time.deltaTime;
            CanShake = false;
            WasShake = true;
            yield return null;
        }

        Camera.localPosition = originalPos;
    }
}