using UnityEngine;
public class Dellvery : MonoBehaviour
{
    [SerializeField] float delayDestory = 1.0f;
    bool hasChicken;

    [SerializeField] Color noChicken = new Color(1, 1, 1, 1);
    [SerializeField] Color haschicken = new Color(0.9f, 0.5f, 0.3f, 1);
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("아야" + collision.gameObject.name, gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("chicken") && !hasChicken)
        {
            Debug.Log("치킨이 픽업 됨");
            hasChicken = true;
            spriteRenderer.color = haschicken;
            Destroy(collision.gameObject, delayDestory);
        }

        if (collision.gameObject.CompareTag("Customer") && hasChicken)
        {
            Debug.Log("치킨이 배달 됨");
            hasChicken = false;
            spriteRenderer.color = noChicken;
        }
    }
}