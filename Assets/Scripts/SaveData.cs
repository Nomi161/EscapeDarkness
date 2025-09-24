using System;
using UnityEngine;

/// <summary>
/// ゲームデータ保存/読み込み処理クラス
/// </summary>
public class SaveData : MonoBehaviour
{
    /// <summary>
    /// ゲームデータをPlayerPrefsに保存するメソッド 
    /// </summary>
    public static void SaveGameData()
    {
        // 現在のstatic変数の状態をGameDataインスタンスにコピー
        GameData dataToSave = new GameData();

        // GameDataインスタンスをJSON文字列に変換
        string jsonData = JsonUtility.ToJson(dataToSave);

        // JSON文字列をPlayerPrefsに保存
        PlayerPrefs.SetString("GameData", jsonData);
        PlayerPrefs.Save(); // 変更をディスクに書き込む

        Debug.Log("セーブしました (JSON): " + jsonData);
    }


    /// <summary>
    /// PlayerPrefsからJSONをロードし、ゲームデータに適用するメソッド 
    /// </summary>
    public static void LoadGameData()
    {
        // PlayerPrefsからJSON文字列をロード
        string jsonData = PlayerPrefs.GetString("GameData");

        // JSON文字列をGameDataインスタンスに変換
        GameData loadedData = JsonUtility.FromJson<GameData>(jsonData);

        // ロードしたデータをstatic変数に適用
        loadedData.ApplyToStatic();
    }
}

// シリアライズは、オブジェクトの状態をバイト列に変換するプロセスです。
// このプロセスにより、オブジェクトはファイルに保存したり、ネットワーク越しに送信したりすることが可能になります。
// 逆のプロセスであるデシリアライズでは、バイト列から元のオブジェクトの状態を復元します
/// <summary>
/// ゲーム保存データ保持クラス
/// </summary>
[Serializable] // JsonUtility でシリアライズ可能（JSON化の準備）にするために必要
public class GameData
{
    public GameState gameState;         // ゲームのステータス

    public bool[] doorsOenedState;      // ドアの開放状況
    public int key1;                    // 鍵１の持数
    public int key2;                    // 鍵２の持数
    public int key3;                    // 鍵３の持数
    public bool[] keysPickedState;      // 鍵の取得状況

    public int bill;                    // お札の持ち数
    public bool[] itemsPickedState;     // アイテムの取得状況

    public bool hasSpotLight;           // スポットライトをもっているかどうか
    public int playerHP;                // プレーヤーのHP

    // RoomManager のデータもここに含める
    public int[] doorsPositionNumber;   // 各入口の配置番号
    public int key1PositionNumber;      // 鍵１の配置番号
    public int[] itemsPositionNumber;   // アイテムの配置番号

    //初期配置が必要かどうか
    public bool positioned; //初回配置が済かどうか

    /// <summary>
    /// コンストラクタで現在のstatic変数の値をコピー 
    /// </summary>
    public GameData()
    {
        gameState = GameManager.gameState;
        doorsOenedState = GameManager.doorsOpenedState;
        key1 = GameManager.key1;
        key2 = GameManager.key2;
        key3 = GameManager.key3;
        keysPickedState = GameManager.keysPickedState;
        bill = GameManager.bill;
        itemsPickedState = GameManager.itemsPickedState;
        hasSpotLight = GameManager.hasSpotLight;
        playerHP = GameManager.playerHP;

        // RoomManager の static 変数もコピー
        doorsPositionNumber = RoomManager.doorsPositionNumber;
        key1PositionNumber = RoomManager.key1PositionNumber;
        itemsPositionNumber = RoomManager.itemsPositionNumber;
        positioned = RoomManager.positioned;
    }

    /// <summary>
    /// static 変数にデータを適用するメソッド 
    /// </summary>
    public void ApplyToStatic()
    {
        GameManager.gameState = gameState;
        GameManager.doorsOpenedState = doorsOenedState;
        GameManager.key1 = key1;
        GameManager.key2 = key2;
        GameManager.key3 = key3;
        GameManager.keysPickedState = keysPickedState;
        GameManager.bill = bill;
        GameManager.itemsPickedState = itemsPickedState;
        GameManager.hasSpotLight = hasSpotLight;
        GameManager.playerHP = playerHP;

        // RoomManager の static 変数に適用
        RoomManager.doorsPositionNumber = doorsPositionNumber;
        RoomManager.key1PositionNumber = key1PositionNumber;
        RoomManager.itemsPositionNumber = itemsPositionNumber;
        RoomManager.positioned = positioned;
    }
}