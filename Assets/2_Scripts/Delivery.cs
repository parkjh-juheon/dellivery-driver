using UnityEngine;
public class Delivery : MonoBehaviour
{
    [SerializeField] float delayDestory = 0.3f;
    
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite peachSprite;
    [SerializeField] Sprite appleSprite;
    [SerializeField] Sprite bananaSprite;
    [SerializeField] Sprite grapeSprite;
    [SerializeField] Sprite orangeSprite;

    enum FruitType { None, Peach, Apple, Banana, Orange, Grape }
    FruitType currentFruit = FruitType.None;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("peach") && currentFruit == FruitType.None)
        {
            Debug.Log("������ �Ⱦ� ��");
            currentFruit = FruitType.Peach;
            spriteRenderer.sprite = peachSprite;
            Destroy(collision.gameObject, delayDestory);
        }

        if (collision.gameObject.CompareTag("CustomerP") && currentFruit == FruitType.Peach)
        {
            Debug.Log("������ ��� ��");
            currentFruit = FruitType.None;
            spriteRenderer.sprite = normalSprite;
        }

        if (collision.gameObject.CompareTag("apple") && currentFruit == FruitType.None)
        {
            Debug.Log("��� �Ⱦ� ��");
            currentFruit = FruitType.Apple;
            spriteRenderer.sprite = appleSprite;
            Destroy(collision.gameObject, delayDestory);
        }

        if (collision.gameObject.CompareTag("CustomerA") && currentFruit == FruitType.Apple)
        {
            Debug.Log("��� ��� ��");
            currentFruit = FruitType.None;
            spriteRenderer.sprite = normalSprite;
        }

        if (collision.gameObject.CompareTag("orange") && currentFruit == FruitType.None)
        {
            Debug.Log("������ �Ⱦ� ��");
            currentFruit = FruitType.Orange;
            spriteRenderer.sprite = orangeSprite;
            Destroy(collision.gameObject, delayDestory);
        }

        if (collision.gameObject.CompareTag("CustomerO") && currentFruit == FruitType.Orange)
        {
            Debug.Log("������ ��� ��");
            currentFruit = FruitType.None;
            spriteRenderer.sprite = normalSprite;
        }

        if (collision.gameObject.CompareTag("banana") && currentFruit == FruitType.None)
        {
            Debug.Log("�ٳ��� �Ⱦ� ��");
            currentFruit = FruitType.Banana;
            spriteRenderer.sprite = appleSprite;
            Destroy(collision.gameObject, delayDestory);
        }

        if (collision.gameObject.CompareTag("CustomerB") && currentFruit == FruitType.Banana)
        {
            Debug.Log("�ٳ��� ��� ��");
            currentFruit = FruitType.None;
            spriteRenderer.sprite = normalSprite;
        }

        if (collision.gameObject.CompareTag("grape") && currentFruit == FruitType.None)
        {
            Debug.Log("���� �Ⱦ� ��");
            currentFruit = FruitType.Grape;
            spriteRenderer.sprite = appleSprite;
            Destroy(collision.gameObject, delayDestory);
        }

        if (collision.gameObject.CompareTag("CustomerG") && currentFruit == FruitType.Grape)
        {
            Debug.Log("���� ��� ��");
            currentFruit = FruitType.None;
            spriteRenderer.sprite = normalSprite;
        }
    }
}