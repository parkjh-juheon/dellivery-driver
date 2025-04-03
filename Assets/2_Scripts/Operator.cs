using UnityEngine;

public class Operator : MonoBehaviour
{
    private void Start()
    {
        int health = 70;

        if (health > 70)
        {
            Debug.Log("건강해요");
        }
        else if (health > 30)
        {
            Debug.Log("약간 지쳤어요");
        }
        else if (health > 0)
        {
            Debug.Log("위험해요");
        }
        else
        {
            Debug.Log("사망");
        }

        float matahScore = 95.0f;
        float englishScore = 85.0f;

        float average = (matahScore + englishScore) / 2;

        if (average > 90)
        {
            Debug.Log("합격");
        }
        else
        {
            Debug.Log("불합격");
        }

        int level = 5;
        bool hasSpecialItem = true;
        bool isInBattle = true;

        if (level >= 5 && hasSpecialItem && isInBattle)
        {
            Debug.Log("아이템 사용 가능");
        }

        else
        {
            Debug.Log("아이템 불사용 가능");
        }
    }
}
