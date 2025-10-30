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
    private Vector3 previousCameraPosition;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다!");
            return;
        }

        previousCameraPosition = mainCamera.transform.position;

        // 수정: 레이어 null 체크
        if (backLayer == null) Debug.LogWarning("Back Layer가 할당되지 않았습니다!");
        if (middleLayer == null) Debug.LogWarning("Middle Layer가 할당되지 않았습니다!");
        if (frontLayer == null) Debug.LogWarning("Front Layer가 할당되지 않았습니다!");
    }

    void Update()
    {
        if (mainCamera == null) return;

        // 카메라 이동량 계산
        Vector3 cameraDelta = mainCamera.transform.position - previousCameraPosition;

        // 각 레이어를 다른 속도로 이동
        if (backLayer != null)
        {
            backLayer.position += new Vector3(cameraDelta.x * backLayerSpeed, 0, 0);
        }

        if (middleLayer != null)
        {
            middleLayer.position += new Vector3(cameraDelta.x * middleLayerSpeed, 0, 0);
        }

        if (frontLayer != null)
        {
            frontLayer.position += new Vector3(cameraDelta.x * frontLayerSpeed, 0, 0);
        }

        previousCameraPosition = mainCamera.transform.position;

        // 수정: 배경 반복 처리 (무한 스크롤)
        CheckAndRepositionLayers();
    }

    // 수정: 배경이 화면 밖으로 나가면 반대편으로 이동 (무한 루프)
    void CheckAndRepositionLayers()
    {
        float cameraX = mainCamera.transform.position.x;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect * 2f;

        CheckRepositionLayer(backLayer, cameraX, screenWidth);
        CheckRepositionLayer(middleLayer, cameraX, screenWidth);
        CheckRepositionLayer(frontLayer, cameraX, screenWidth);
    }

    void CheckRepositionLayer(Transform layer, float cameraX, float screenWidth)
    {
        if (layer == null) return;

        // 배경의 왼쪽 끝이 화면 왼쪽을 완전히 벗어났는지 확인
        SpriteRenderer spriteRenderer = layer.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float layerWidth = spriteRenderer.bounds.size.x;
            float layerRightEdge = layer.position.x + layerWidth / 2f;

            // 배경이 화면 왼쪽을 벗어나면 오른쪽으로 재배치
            if (layerRightEdge < cameraX - screenWidth)
            {
                layer.position = new Vector3(
                    layer.position.x + layerWidth * 2f,
                    layer.position.y,
                    layer.position.z
                );
            }
        }
    }
}
