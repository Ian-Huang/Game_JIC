using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public float speed = 5;                         //物體移動速度
    public Vector3 Direction = Vector3.zero;        //物體移動方向(使用Unity世界座標)

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.Direction * Time.deltaTime * this.speed;
    }
}