using Unity.VisualScripting;
using UnityEngine;

public class BillController : MonoBehaviour
{
    public float deleteTime = 2.0f;     // 自動発動までの時間
    public GameObject barrierPrefab;    // 事故消滅と引き換えに生成するプレハブ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // deleteTime秒後に「バリア展開して消滅」
        Invoke("FieldExpansion", deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// バリア展開と事故消滅を行うメソッド
    /// </summary>
    void FieldExpansion()
    {
        Instantiate(barrierPrefab, transform.position, Quaternion.identity);    // お札と同じ場所にバリア発生
        Destroy(gameObject); // お札は消滅
    }

    // 敵とぶつかったらバリア発動
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FieldExpansion();
        }
    }
}
