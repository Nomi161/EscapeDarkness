using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// プレイヤーが出てきた時の方向
public enum DoorDirection
{
    up,
    down
}
public class RoomData : MonoBehaviour
{
    public string roomName;         // 出入り口の識別名
    public string nextRoomName;     // シーン切り替え先での行先
    public string nextScene;        // シーン切り替え先
    public bool openedDoor;         // ドアの開閉状況フラグ
    public DoorDirection direction; // プレイヤーの配置位置
    public MessageData message;     // トークデータ
    public GameObject door;         // 表示/非表示対象のドア情報
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 相手がプレイヤーなら
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        // このRoomに触れたらどこに行くのかを変数nextRoomNameで決めておく
        // シーンが切り替わって情報がれセットされる前に
        // static変数であるtoRoomNumberに行き先情報を記録
        RoomManager.toRoomNumber = nextRoomName;

        SceneManager.LoadScene(nextScene);

    }

    // ドアの開閉状況をチェックするメソッド
    public  void DoorOpenCheck()
    {
        if (openedDoor) door.SetActive(false);
    }
}
