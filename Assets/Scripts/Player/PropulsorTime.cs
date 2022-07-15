using UnityEngine;

public class PropulsorTime : MonoBehaviour
{
    public delegate void setSlider(float amount);

    public delegate void stopFlying();

    public static setSlider setSlide;
    public static stopFlying stopFly;

    [SerializeField] private float starTime = 20;

    [SerializeField] private CollisionDetector2D groundCheck;

    [SerializeField] private CollisionDetector2D wallCheck;

    private OldPlayerController _oldPlayer;
    private bool restar;
    private float time = 20;

    private void Awake()
    {
        time = starTime;
        _oldPlayer = GetComponent<OldPlayerController>();
    }

    private void FixedUpdate()
    {
        if (restar)
            voidTank();
        else
            fillTank();
        setSlide?.Invoke(time / starTime);
        if (time <= 0)
        {
            _oldPlayer.tankFilled = false;
            stopFly?.Invoke();
        }
    }

    private void OnEnable()
    {
        _oldPlayer.Jumping += startFill;
        _oldPlayer.StopJumping += startVoid;
    }

    private void OnDisable()
    {
        _oldPlayer.Jumping -= startFill;
        _oldPlayer.StopJumping -= startVoid;
    }

    public void ResetTimer()
    {
        time = starTime;
    }

    public void fillTank()
    {
        _oldPlayer.tankFilled = true;
        if (time < starTime)
            time += Time.deltaTime / 2;
        else
            time = starTime;
    }

    public void voidTank()
    {
        if (time > 0)
            time -= Time.deltaTime;
        else
            time = 0;
    }

    public void startFill()
    {
        restar = true;
    }

    private void startVoid()
    {
        restar = false;
    }
}