using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public float deleteTime = 5.0f; // 消滅するまでの時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // deleteTime秒後に消滅
        Destroy(gameObject, deleteTime);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
