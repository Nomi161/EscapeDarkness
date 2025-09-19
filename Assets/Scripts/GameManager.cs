using UnityEngine;

// ゲーム状態を管理する列挙型
public enum GameState
{
    playing,
    talk,
    gameover,
    gameclear,
    ending
}

public class GameManager : MonoBehaviour
{
    public static GameState gameState;      // ゲームのステータス
    public static bool[] doorsOpenedState = {false,false,false};  // ドアの開放状況
    public static int key1;                 // 鍵１の餅数
    public static int key2;                 // 鍵２の餅数
    public static int key3;                 // 鍵３の餅数
    public static bool[] keyssPickedState = { false,false,false};  // 鍵の取得状況

    public static int bill = 10;            // お札の持ち数
    public static bool[] itemsPickedState = {false,false,false,false,false};  // アイテムの取得状況
                                            // 
    public static bool hasSpotLight;        // スポットライトをもっているかどうか
    public static int playerHP = 3;             // プレーヤーのHP

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // まずはゲームは開始状態にする
        gameState = GameState.playing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
