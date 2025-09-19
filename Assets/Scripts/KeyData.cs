using UnityEngine;

// 自作のキーを判別する型
public enum KeyType
{
    key1,
    key2,
    key3
}
public class KeyData : MonoBehaviour
{
    public KeyType keyType = KeyType.key1;  // 識別タイプ
    Rigidbody2D rbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // keyのType次第で該当する鍵の所持数を増やす
            switch (keyType)
            {
                case KeyType.key1:
                    GameManager.key1++;
                    GameManager.keyssPickedState[0] = true;
                    break;
                case KeyType.key2:
                    GameManager.key2++;
                    GameManager.keyssPickedState[1] = true;
                    break;
                case KeyType.key3:
                    GameManager.key3++;
                    GameManager.keyssPickedState[2] = true;
                    break;
            }

            // アイテム取得演出
            // ①コライダーを無効化
            GetComponent<CircleCollider2D>().enabled = false;
            // ②Rigidbody2Dの復活（Dynamicにする）
            rbody.bodyType = RigidbodyType2D.Dynamic;
            // ③上に打ち上げ（上向き5の力）
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            // ④自分自身（オブジェクトごと）を抹消（0.5秒後）
            Destroy(gameObject, 0.5f);
        }
    }
}
