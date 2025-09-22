using System.Globalization;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static int[] doorsPositionNumber = {0, 0, 0};        // 各入口の配置番号
    public static int  key1PositionNumber;                      // 鍵１の配置番号
    public static int[] itemPositionNumber = {0, 0, 0, 0, 0};   // アイテムの配置番号

    public GameObject[] items = new GameObject[5];  // ５つのアイテムプレハブの内訳
    public GameObject room;                         // ドアのプレハブ
    public MessageData[] messages;                  // 配置したドアに割り振るScriptableObjectの配列

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
            StartItemsPosition();   // アイテムの初回配置
            StartDoorsPosition();   // ドアの初回配置
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

    void StartDoorsPosition()
    {
        // 全スポットの取得
        GameObject[] roomSpots = GameObject.FindGameObjectsWithTag("RoomSpot");

        // 出入り口(鍵1～鍵3の3つの出入り口)の分だけ繰り返し
        for (int i = 0; i < doorsPositionNumber.Length; i++)
        {
            int rand;       // ランダムな数の受け皿
            bool unique;    // 重複していないかのフラグ

            do
            {
                unique = true;  // 問題なければそのままループを抜ける予定
                rand = Random.Range(1, (roomSpots.Length + 1)); // 1晩からスポット数の番号をランダムで取得

                // すでにランダムに取得した番号がどこかのスポットとして割り当てられていないか、doorsPositionNumber配列の状況を全点検
                foreach (int numbers in doorsPositionNumber)
                {
                    // 取り出した情報とランダム番号が一致していたら重複したということになる
                    if (numbers == rand)
                    {
                        unique = false; // 唯一のユニークな物では無い
                        break;
                    }
                }
            } while (!unique);

            // 全スポットを見回りしてrandと同じスポットを探す
            foreach (GameObject spots in roomSpots)
            {
                if (spots.GetComponent<RoomSpot>().spotNum == rand)
                {
                    // ルームを生成
                    GameObject obj = Instantiate(
                        room,
                        spots.transform.position,
                        Quaternion.identity
                        );

                    // 何番スポットが選ばれたのかstatic変数に記憶していく
                    doorsPositionNumber[i] = rand;
                }
            }
        }
    }
}
