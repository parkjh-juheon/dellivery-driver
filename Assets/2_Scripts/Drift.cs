using UnityEngine;

public class Drift : MonoBehaviour
{
    [Header("전진/후진 가속도")] public float acceleration = 20f;
    [Header("조향 속도")] public float steering = 3f;
    [Header("낮을수록 더 미끄러짐")] public float driftFactor = 0.95f;
    [Header("최대 속도 제한")] public float maxSpeed = 10f;

    [SerializeField] float slowAccelerationRaito = 0.5f;
    [SerializeField] float boostAccelerationRaito = 1.5f;
    [SerializeField] float speedTime = 3f;

    float defaulAcceleration;
    float slowAcceleration;
    float boostAcceleration;

    public ParticleSystem smokeLeft;
    public ParticleSystem smokeRight;

    public float driftThreshold = 1.5f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    public TrailRenderer leftTrail;
    public TrailRenderer rightTrail;
    private SpriteRenderer spriteRenderer;
    private float previousSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = rb.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        defaulAcceleration = acceleration;
        slowAcceleration = acceleration * slowAccelerationRaito;
        boostAcceleration = acceleration * boostAccelerationRaito;
    }

    private void FixedUpdate()
    {
        float speed = Vector2.Dot(rb.linearVelocity, transform.up);
        if (speed < maxSpeed)
        {
            rb.AddForce(transform.up * Input.GetAxis("Vertical") * acceleration);
        }

        // 조향 입력
        //float turnAmount = Input.GetAxis("Horizontal") * steering * speed * Time.fixedDeltaTime;
        float turnAmount = Input.GetAxis("Horizontal") * steering * Mathf.Clamp(speed / maxSpeed, 0.4f, 1f);
        rb.MoveRotation(rb.rotation - turnAmount); // Z축 회전

        // 드리프트 적용
        ApplyDrift();
    }

    void ApplyDrift()
    {
        // 현재 속도를 차체 기준으로 나눔
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 sideVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

        // 옆으로 미끄러지는 속도를 줄임 (마찰처럼)
        rb.linearVelocity = forwardVelocity + sideVelocity * driftFactor;
    }

    private void Update()
    {
        float sidewaysVelocity = Vector2.Dot(rb.linearVelocity, transform.right);
        bool isDrifting = Mathf.Abs(sidewaysVelocity) > driftThreshold && rb.linearVelocity.magnitude > 2f;

        if (isDrifting)
        {
            if (!audioSource.isPlaying) audioSource.Play();
            if (!smokeLeft.isPlaying) smokeLeft.Play();
            if (!smokeRight.isPlaying) smokeRight.Play();
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            if (smokeLeft.isPlaying) smokeLeft.Stop();
            if (smokeRight.isPlaying) smokeRight.Stop();
        }

        audioSource.volume = Mathf.Lerp(audioSource.volume, isDrifting ? 1.0f : 0.0f, Time.deltaTime * 5f);
        leftTrail.emitting = isDrifting;
        rightTrail.emitting = isDrifting;

        // 현재 속도 계산
        float currentSpeed = rb.linearVelocity.magnitude;

        // 속도 변화가 클 때만 출력 (작은 흔들림은 무시)
        if (Mathf.Abs(currentSpeed - previousSpeed) > 0.5f)
        {
            if (currentSpeed > previousSpeed)
            {
                Debug.Log("속도가 빨라졌습니다! 🚗💨");
            }
            else
            {
                Debug.Log("속도가 느려졌습니다... 🐌");
            }
        }

        // 이전 속도 갱신
        previousSpeed = currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boost"))
        {
            acceleration = boostAcceleration; 
            Debug.Log("Boooost!!");

            Invoke("ResetAcceleration", speedTime);
        }

        if (collision.gameObject.CompareTag("Road"))
        {
            acceleration = slowAcceleration * 0.3f;
            Debug.Log("이탈함");

            Color currentColor = spriteRenderer.color;
            Color darkerColor = currentColor * 0.9f;
            darkerColor.a = currentColor.a;
            spriteRenderer.color = darkerColor;
        }
    }

    void ResetAcceleration()
    {
        acceleration = defaulAcceleration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        acceleration = slowAcceleration;
        Debug.Log("느려");

        if (spriteRenderer != null)
        {
            Color currentColor = spriteRenderer.color;
            Color darkerColor = currentColor * 0.9f;
            darkerColor.a = currentColor.a;
            spriteRenderer.color = darkerColor;

            if (IsColorTooDark(darkerColor))
            {
                GameOver();
            }
        }

        Invoke("ResetAcceleration", speedTime);

        bool IsColorTooDark(Color color)
        {
            return color.r <= 0.2f && color.g <= 0.2f && color.b <= 0.2f;
        }

        void GameOver()
        {
            Debug.Log("게임 오버!");
            Time.timeScale = 0; // 일시정지
        }
    }
}
