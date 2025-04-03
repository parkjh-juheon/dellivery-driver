using UnityEngine;

public class House
{
    public string tv = "거실  TV";
    private string diary = "비밀 다이어리";
    protected string secreKey = "집 비밀번호";


    public string GetDiary()
    {
        return diary;
    }
}