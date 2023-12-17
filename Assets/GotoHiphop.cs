using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoHiphop : MonoBehaviour
{
    // 전환할 씬의 이름을 여기에 입력하세요.
    public string targetSceneName = "Hiphop";

    private void Start()
    {
        // 버튼 클릭 시 OnButtonClick 메서드 호출 등록
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // 버튼 클릭 시 호출될 메서드
    private void OnButtonClick()
    {
        Debug.Log("Button Clicked"); // 디버그 로그 추가
        SceneManager.LoadScene(targetSceneName);
    }
}