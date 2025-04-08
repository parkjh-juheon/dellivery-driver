using UnityEngine;

public class Drift : MonoBehaviour
{
    [Header("����/���� ���ӵ�")] public float acceleration = 20f;
    [Header("���� �ӵ�")] public float steering = 3f;
    [Header("�������� �� �̲�����")] public float driftFactor = 0.95f;
    [Header("�ִ� �ӵ� ����")] public float maxSpeed = 10f;

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

        // ���� �Է�
        //float turnAmount = Input.GetAxis("Horizontal") * steering * speed * Time.fixedDeltaTime;
        float turnAmount = Input.GetAxis("Horizontal") * steering * Mathf.Clamp(speed / maxSpeed, 0.4f, 1f);
        rb.MoveRotation(rb.rotation - turnAmount); // Z�� ȸ��

        // �帮��Ʈ ����
        ApplyDrift();
    }

    void ApplyDrift()
    {
        // ���� �ӵ��� ��ü �������� ����
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 sideVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

        // ������ �̲������� �ӵ��� ���� (����ó��)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boost"))
        {
            acceleration = boostAcceleration; // �� ���� ����!
            Debug.Log("Boooost!!");

            Invoke("ResetAcceleration", speedTime);
        }
    }

    void ResetAcceleration()
    {
        acceleration = defaulAcceleration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        acceleration = slowAcceleration;
        Debug.Log("����");

        if (spriteRenderer != null)
        {
            Color currentColor = spriteRenderer.color;

            // ���� ���� �� ���ϰ� (��⸦ ����)
            Color darkerColor = currentColor * 0.9f; // R, G, B ��� 10% ��Ӱ�

            // ����(����)�� ����
            darkerColor.a = currentColor.a;

            spriteRenderer.color = darkerColor;
        }

        Invoke("ResetAcceleration", speedTime);
    }
}