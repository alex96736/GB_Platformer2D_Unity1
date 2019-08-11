using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform Player;

    // Точки ограничения движения камеры
    public Pose left;
    public Pose right;
    public Pose up;
    public Pose down;
    private float x, y;

    void Start()
    {
        
    }

    
    void Update()
    {
        // проврека движения камеры по Х
        if(Player.transform.position.x <= right.position.x && Player.transform.position.x >= left.position.x)
        {
            x = Player.position.x;
        } 
        else
        {
            if (Player.transform.position.x > right.position.x)
            {
                x = right.position.x;
            }
            if (Player.transform.position.x < left.position.x)
            {
                x = left.position.x;
            }

        }
        // проврека движения камеры по Y
        if(Player.transform.position.y <= up.position.y && Player.transform.position.y >= down.position.y)
        {
            y = Player.position.y;
        } 
        else
        {
            if(Player.transform.position.y > up.position.y)
            {
                y = up.position.y;
            }
            if(Player.transform.position.y < down.position.y)
            {
                y = down.position.y;
            }   
        }
        // Задание положения камеры
        transform.position = new Vector3(x, y, transform.position.z);
    }

    /// <summary>
    /// Вывод в правом верхнем углу значений HP
    /// </summary>
    void OnGUI()
    {
        GUI.BeginGroup(new Rect(10, 10, 200, 200));
        GUI.Label(new Rect(0, 0, 50, 20), $"HP: {Player.GetComponent<Player>().GetHP()}");
        GUI.EndGroup();
    }

}
