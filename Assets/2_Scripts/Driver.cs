using UnityEngine;
using System.Collections; // 코루틴 사용을 위해 필요

public class Driver : MonoBehaviour
{
    [SerializeField, Range(-0.5f, 100), Header("회전")]
    private float turnSpeed = 0.1f;

    [SerializeField, Range(0.01f, 30), Header("움직임")]
    public float moveSpeed = 0.01f;

    [SerializeField] float slowSpeedRatio = 0.5f;
    [SerializeField] float boostSpeedRatio = 1.5f;
    [SerializeField] float slowDuration = 2f; // 느려지는 지속 시간 (2초)
    [SerializeField] float boostDuration = 3f; // 부스터 지속 시간 (3초)

    private float defaultSpeed; // 원래 속도 저장
    private float slowSpeed;
    private float boostSpeed;

    private void Start()
    {
        defaultSpeed = moveSpeed; // 초기 속도 저장
        slowSpeed = moveSpeed * slowSpeedRatio;
        boostSpeed = moveSpeed * boostSpeedRatio;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            moveSpeed = boostSpeed;
            Debug.Log("Boooossst!!!!");

            // 일정 시간 후 속도를 원래대로 복구하는 코루틴 실행
            StartCoroutine(ResetSpeedAfterDelay(boostDuration));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed = slowSpeed;

        StartCoroutine(ResetSpeedAfterDelay(slowDuration));
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
        moveSpeed = defaultSpeed; // 원래 속도로 복구
        Debug.Log("부스터 효과 종료!");
    }
}
