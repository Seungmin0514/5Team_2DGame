using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("배경 레이어 설정")]
    [Tooltip("가장 먼 배경 (하늘, 산 등) - 가장 느리게 움직임")]
    public Transform backLayer;      // 3층: 가장 먼 배경

    [Tooltip("중간 배경 (나무, 건물 등) - 중간 속도")]
    public Transform middleLayer;    // 2층: 중간 배경

    [Tooltip("가까운 배경 (풀, 돌 등) - 가장 빠르게 움직임")]
    public Transform frontLayer;     // 1층: 가까운 배경

    [Header("이동 속도 설정 (0~1 사이 값)")]
    [Range(0f, 1f)]
    [Tooltip("0에 가까울수록 느리게, 1에 가까울수록 빠르게")]
    public float backLayerSpeed = 0.2f;     // 가장 느림

    [Range(0f, 1f)]
    public float middleLayerSpeed = 0.5f;   // 중간

    [Range(0f, 1f)]
    public float frontLayerSpeed = 0.8f;    // 가장 빠름

    [Header("참조 설정")]
    public float baseSpeed = 5f;  // 맵의 기본 이동 속도 (MapGenerator의 mapSpeed와 동일하게 설정)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다!");
            return;
        }

        // 수정: 레이어 null 체크
        if (backLayer == null) Debug.LogWarning("Back Layer가 할당되지 않았습니다!");
        if (middleLayer == null) Debug.LogWarning("Middle Layer가 할당되지 않았습니다!");
        if (frontLayer == null) Debug.LogWarning("Front Layer가 할당되지 않았습니다!");
    }

    void Update()
    {
        if (mainCamera == null) return;

        // 수정: 시간 기반으로 배경을 왼쪽으로 이동
        float deltaTime = Time.deltaTime;

        // 각 레이어를 다른 속도로 왼쪽으로 이동
        if (backLayer != null)
        {
            backLayer.position += Vector3.left * baseSpeed * backLayerSpeed * deltaTime;
            CheckAndRepositionLayer(backLayer); // 수정: 매 프레임 체크
        }

        if (middleLayer != null)
        {
            middleLayer.position += Vector3.left * baseSpeed * middleLayerSpeed * deltaTime;
            CheckAndRepositionLayer(middleLayer); // 수정: 매 프레임 체크
        }

        if (frontLayer != null)
        {
            frontLayer.position += Vector3.left * baseSpeed * frontLayerSpeed * deltaTime;
            CheckAndRepositionLayer(frontLayer); // 수정: 매 프레임 체크
        }
    }

    // 수정: 레이어의 자식 스프라이트들을 체크하고 재배치
    void CheckAndRepositionLayer(Transform layer)
    {
        if (layer == null || layer.childCount == 0) return;

        // 카메라의 왼쪽 끝 계산 (여유 공간 추가)
        float cameraLeftEdge = mainCamera.transform.position.x - (mainCamera.orthographicSize * mainCamera.aspect) - 5f;

        foreach (Transform child in layer)
        {
            if (child == null) continue;

            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) continue;

            float spriteWidth = spriteRenderer.bounds.size.x;
            float spriteRightEdge = child.position.x + spriteWidth / 2f;

            // 수정: 스프라이트의 오른쪽 끝이 화면 왼쪽을 완전히 벗어났는지 확인
            if (spriteRightEdge < cameraLeftEdge)
            {
                // 수정: 같은 레이어 내에서 가장 오른쪽에 있는 스프라이트 찾기
                float maxX = float.MinValue;
                Transform rightmostSprite = null;

                foreach (Transform other in layer)
                {
                    if (other != null && other != child)
                    {
                        if (other.position.x > maxX)
                        {
                            maxX = other.position.x;
                            rightmostSprite = other;
                        }
                    }
                }

                // 수정: 가장 오른쪽 스프라이트 옆에 재배치
                if (rightmostSprite != null)
                {
                    SpriteRenderer rightRenderer = rightmostSprite.GetComponent<SpriteRenderer>();
                    float rightWidth = rightRenderer != null ? rightRenderer.bounds.size.x : spriteWidth;

                    // 수정: 틈이 생기지 않도록 정확히 붙여서 배치
                    child.position = new Vector3(
                        rightmostSprite.position.x + rightWidth,
                        child.position.y,
                        child.position.z
                    );

                    Debug.Log($"{child.name} 재배치 완료! 새 위치: {child.position.x}");
                }
            }
        }
    }
}
