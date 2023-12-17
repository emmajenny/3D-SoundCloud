using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    public GameObject uiPanel; // 연결할 UI 패널

    private void Start()
    {
        // UI 패널을 비활성화
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            // Ray를 통해 마우스 클릭 지점 감지
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 클릭한 지점에 Collider가 있는지 확인
            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 오브젝트가 현재 스크립트가 부착된 오브젝트인지 확인
                if (hit.collider.gameObject == this.gameObject)
                {
                    // 클릭한 오브젝트가 현재 스크립트가 부착된 오브젝트이면 UI 패널을 활성화
                    if (uiPanel != null)
                    {
                        uiPanel.SetActive(true);
                    }
                }
            }
        }
    }
}
