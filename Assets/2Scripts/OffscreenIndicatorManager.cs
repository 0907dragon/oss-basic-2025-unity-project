using UnityEngine;

public class OffscreenIndicatorManager : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public Transform player;
    public RectTransform canvasRect;
    public RectTransform indicatorPrefab;

    [Header("Settings")]
    public float edgePadding = 50f;   // 화면 중앙에서 화살표까지의 여유

    private RectTransform indicator;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (canvasRect == null)
            canvasRect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // 화살표 하나만 만들어서 계속 재사용
        indicator = Instantiate(indicatorPrefab, canvasRect);
        indicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (EnemyManager.Instance == null ||
            EnemyManager.Instance.enemies.Count == 0)
        {
            indicator.gameObject.SetActive(false);
            return;
        }

        Transform enemy = GetClosestOffscreenEnemy();

        // 화면 밖 적이 없으면 숨김
        if (enemy == null)
        {
            indicator.gameObject.SetActive(false);
            return;
        }

        indicator.gameObject.SetActive(true);
        UpdateIndicatorPosition(enemy);
    }

    // 가장 가까운 "화면 밖" Enemy 찾기
    private Transform GetClosestOffscreenEnemy()
    {
        Transform result = null;
        float minDist = float.MaxValue;

        foreach (Transform enemy in EnemyManager.Instance.enemies)
        {
            if (enemy == null) continue;

            Vector3 vp = mainCamera.WorldToViewportPoint(enemy.position);

            // 카메라 뒤쪽이면 무조건 화면 밖 취급
            bool isBehind = vp.z < 0f;

            bool inside =
                vp.x >= 0f && vp.x <= 1f &&
                vp.y >= 0f && vp.y <= 1f;

            bool isOffscreen = isBehind || !inside;
            if (!isOffscreen) continue;

            float dist = Vector3.Distance(player.position, enemy.position);
            if (dist < minDist)
            {
                minDist = dist;
                result = enemy;
            }
        }

        return result;
    }

    // 화면 중심을 기준으로 원형 경계 위에 화살표 배치
    private void UpdateIndicatorPosition(Transform enemy)
    {
        Vector3 vp = mainCamera.WorldToViewportPoint(enemy.position);

        // 카메라 뒤에 있으면 반전
        if (vp.z < 0f)
        {
            vp.x = 1f - vp.x;
            vp.y = 1f - vp.y;
        }

        // 뷰포트(0~1) 기준에서 화면 중심(0,0)으로 이동
        Vector2 fromCenter = new Vector2(vp.x - 0.5f, vp.y - 0.5f);

        if (fromCenter.sqrMagnitude < 0.0001f)
            fromCenter = Vector2.up;

        fromCenter.Normalize();

        // 캔버스에서 쓸 반지름(가장 짧은 변의 절반 - 패딩)
        float halfW = canvasRect.rect.width * 0.5f;
        float halfH = canvasRect.rect.height * 0.5f;
        float radius = Mathf.Min(halfW, halfH) - edgePadding;

        // 중앙에서 방향벡터 * 반지름 → 앵커드포지션
        Vector2 localPos = fromCenter * radius;
        indicator.anchoredPosition = localPos;

        // 방향을 따라 회전 (스프라이트 기본이 ↑ 라고 가정)
        float angle = Mathf.Atan2(fromCenter.y, fromCenter.x) * Mathf.Rad2Deg - 90f;
        indicator.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
