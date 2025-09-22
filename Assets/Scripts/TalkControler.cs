using System.Collections;
using TMPro;
using UnityEngine;

public class TalkControler : MonoBehaviour
{
    public MessageData message;     // ScriptableObjectであるクラス
    bool isPlayerInRange;           // プレイヤーが領域に入ったかどうか
    bool isTalk;                    // トークが開始されたかどうか
    GameObject canvas;              // トークUIを含んだCanvasオブジェクト
    GameObject talkPanel;           // 対象となるトークURパネル
    TextMeshProUGUI nameText;       // 対象となるトークUIパネルの名前
    TextMeshProUGUI messageText;    // 対象となるトークUIパネルのメッセージ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        talkPanel = canvas.transform.Find("TalkPanel").gameObject;
        nameText = talkPanel.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        messageText = talkPanel.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && !isTalk && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();    // トーク開始
        }
    }

    // トークを開始してゲームスピードをストップさせるメソッド
    void StartConversation()
    {
        isTalk = true;  // トーク中フラグを立てる
        GameManager.gameState = GameState.talk; // ステータスをtalk
        talkPanel.SetActive(true);  // トークUIパネルを表示
        Time.timeScale = 0f;    // ゲーム振興スピードを0

        // TalkProcessコルーチンの発動
        StartCoroutine(TalkProcess());

    }

    // TalkProcessコルーチンの作成
    IEnumerator TalkProcess()
    {
        // 対象としたScriptableObject(変数message)が扱っている配列
        for(int i = 0; i < message.msgArray.Length; i++)
        {
            nameText.text = message.msgArray[i].name;
            messageText.text = message.msgArray[i].message;

            //yield return new WaitForSeconds(0.1f);    // ゲームスピードが0でなければ通常こちらを使う
            yield return new WaitForSecondsRealtime(0.1f);  // 0.1秒待つ ※Unityではメソッドを抜けずに 指定された処理が終了するまで待機する

            while (!Input.GetKeyDown(KeyCode.E))
            {
                // ※次の構文 "yield return null;"は通常のC＃ではnull値を返すイテレーターになりますが
                //   Unity ではIEnumeratorを使ったコルーチンの中では次のフレームまで待機するという意味になります。
                yield return null;  // 何もしない
            }

            EndConversation();    // トーク終了の処理
        }
    }

    void EndConversation()
    {
        talkPanel.SetActive(false);  // パネルを非表示
        GameManager.gameState = GameState.playing;  // ゲームステータスをplayingにも押し
        isTalk = false; // トーク中を解除
        Time.timeScale = 1.0f;  // ゲームスピードをもとにもどす
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // フラグがON
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // フラグがOFF
            isPlayerInRange = false;
        }
    }

}
