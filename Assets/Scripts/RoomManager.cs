using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int[] doorsPositionNumber = {0, 0, 0};        // 各入口の配置番号
    public static int  key1PositionNumber;                      // 鍵１の配置番号
    public static int[] itemPositionNumber = {0, 0, 0, 0, 0};   // アイテムの配置番号

    public GameObject[] items = new GameObject[5];  // ５つのアイテムプレハブの内訳
    public GameObject room;                         // ドアのプレハブ
    public GameObject dummyDoor;                    // ダミードアのプレハブ
    public GameObject key;                          // キーのプレハブ

    public static bool positioned;                      // 初回配置が済かどうか
    public static string toRoomNumber = "fromRoom1";    // Playerが配置されるべき位置

    GameObject player;  // プレイヤーの情報
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // プレイヤー情報の取得
        player = GameObject.FindGameObjectWithTag("Player");

        if (!positioned)    // 初期配置がまだ
        {
            StartKeysPosition();    // キーの初回配置
            StartItemsPosition();   // アイテムの初期配置
            positioned = true;      // 初回配置は済み
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartKeysPosition()
    {
        // 全Key1スポットの取得
        GameObject[] keySpots = GameObject.FindGameObjectsWithTag("KeySpot");

        // ランダムに番号を取得（第一引数以上 第二引数未満）
        int rand = Random.Range(1, (keySpots.Length + 1));

        // 全スポットをチェックしに行く
        foreach (GameObject spots in keySpots)
        {
            // ひとつひとつspotNumの中身を確認してrandと同じかチェック
            if (spots.GetComponent<KeySpot>().spotNum == rand)
            {
                // キー1を生成
                Instantiate(key, spots.transform.position, Quaternion.identity);
                // どのスポット番号にキーを配置したか記録
                key1PositionNumber = rand;
            }
        }

        // Key2およびKey3の対象スポット
        GameObject keySpot;
        GameObject obj;     // 生成したKey2、およびKey3が入る予定

        // Key2スポットの取得
        keySpot = GameObject.FindGameObjectWithTag("KeySpot2");
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        // 生成したKeyのタイプをkey2に変更
        obj.GetComponent<KeyData>().keyType = KeyType.key2;

        // Key3スポットの取得
        keySpot = GameObject.FindGameObjectWithTag("KeySpot3");
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        // 生成したKeyのタイプをkey3に変更
        obj.GetComponent<KeyData>().keyType = KeyType.key3;
    }

    void StartItemsPosition()
    {
        // 全部のアイテムスポットを取得
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for (int i = 0; i < items.Length; i++)
        {
            // ランダムな数字の取得
            // ※ただしアイテム割り振り済みの番号を引いたら、ランダム引き直し
            // スポットの全チェック（ランダム値とスポット番号の一致）
            // 一致していれば、そこにアイテムを生成
            // どのスポット番号がどのアイテムに割り当てられているのかを記録
            // 生成したアイテムに識別番号を割り振っていく
        }
}
