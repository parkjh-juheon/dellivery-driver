using UnityEngine;

public class Operator : MonoBehaviour
{
    private void Start()
    {
        int health = 70;

        if (health > 70)
        {
            Debug.Log("�ǰ��ؿ�");
        }
        else if (health > 30)
        {
            Debug.Log("�ణ ���ƾ��");
        }
        else if (health > 0)
        {
            Debug.Log("�����ؿ�");
        }
        else
        {
            Debug.Log("���");
        }

        float matahScore = 95.0f;
        float englishScore = 85.0f;

        float average = (matahScore + englishScore) / 2;

        if (average > 90)
        {
            Debug.Log("�հ�");
        }
        else
        {
            Debug.Log("���հ�");
        }

        int level = 5;
        bool hasSpecialItem = true;
        bool isInBattle = true;

        if (level >= 5 && hasSpecialItem && isInBattle)
        {
            Debug.Log("������ ��� ����");
        }

        else
        {
            Debug.Log("������ �һ�� ����");
        }
    }
}
