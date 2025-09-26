using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;    // 切り替えたいシーン名を氏名
    public bool toTitle;        // タイトルへの切り替えかどうかのフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // シーンを切り替える機能をもったメソッド作成
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // シーンが切り替わる際はいずれにしてもステージスコアはリセット
        //GameManager.stageScore = 0;

        // toTitleフラグがtrueになっている場合はタイトルに戻ることが予想されるのでトータルスコアもリセット
        //if (toTitle) GameManager.totalScore = 0;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter2D()");
            // 引数に指定した名前のシーン切り替えのメソッド呼び出し
            SceneManager.LoadScene(sceneName);
        }
    }
}
