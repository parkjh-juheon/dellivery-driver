using UnityEngine;
using System.Collections; // �ڷ�ƾ ����� ���� �ʿ�

public class Driver : MonoBehaviour
{
    [SerializeField, Range(-0.5f, 100), Header("ȸ��")]
    private float turnSpeed = 0.1f;

    [SerializeField, Range(0.01f, 30), Header("������")]
    public float moveSpeed = 0.01f;

    [SerializeField] float slowSpeedRatio = 0.5f;
    [SerializeField] float boostSpeedRatio = 1.5f;
    [SerializeField] float slowDuration = 2f; // �������� ���� �ð� (2��)
    [SerializeField] float boostDuration = 3f; // �ν��� ���� �ð� (3��)

    private float defaultSpeed; // ���� �ӵ� ����
    private float slowSpeed;
    private float boostSpeed;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        defaultSpeed = moveSpeed; // �ʱ� �ӵ� ����
        slowSpeed = moveSpeed * slowSpeedRatio;
        boostSpeed = moveSpeed * boostSpeedRatio;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            moveSpeed = boostSpeed;
            Debug.Log("Boooossst!!!!");

            // ���� �ð� �� �ӵ��� ������� �����ϴ� �ڷ�ƾ ����
            StartCoroutine(ResetSpeedAfterDelay(boostDuration));
        }

        if (other.CompareTag("Road"))
        {
            moveSpeed = defaultSpeed;
            Debug.Log("���� ����: ���� �ӵ�");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Road"))
        {
            moveSpeed = slowSpeed;
            Debug.Log("���� ���: ������");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed = slowSpeed;
        StartCoroutine(ResetSpeedAfterDelay(slowDuration));

        // �� ���ϰ�
        if (spriteRenderer != null)
        {
            Color currentColor = spriteRenderer.color;
            Color darker = currentColor * 0.9f;
            darker.a = currentColor.a;
            spriteRenderer.color = darker;
        }
    }


    void Update()
    {
        float turnAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -turnAmount);
        transform.Translate(0, moveAmount, 0);
    }

    IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveSpeed = defaultSpeed; // ���� �ӵ��� ����
        Debug.Log("�ν��� ȿ�� ����!");
    }
}
