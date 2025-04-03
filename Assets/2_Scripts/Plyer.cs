using UnityEngine;

public class Plyer
{

    public int health = 100;
    public static int PlayerCount = 0;

    public Plyer()
    {
        PlayerCount++;
    }
    public void TakeDamage(int damage)
    {
        health = health - damage;
    }

    public void Attack()
    {
        int damage = 10;
        Debug.Log("공격력" +  damage);
    }

    public void Defend()
    {
        int damge = 5;
            Debug.Log("방어력" +  damge);
    }

}
