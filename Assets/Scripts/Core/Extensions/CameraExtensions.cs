using UnityEngine;

public static class CameraExtensions
{
    public static float GetPositionX(this Camera camera) => camera.transform.position.x;

    public static float GetPositionY(this Camera camera) => camera.transform.position.y;

    public static float GetWorldWidth(this Camera camera) => camera.GetWorldHeight() * camera.aspect;

    public static float GetWorldHeight(this Camera camera) => camera.orthographicSize * 2.0f;

    public static float GetLeftEdge(this Camera camera) => camera.GetPositionX() - (camera.GetWorldWidth() / 2.0f);

    public static float GetRightEdge(this Camera camera) => camera.GetPositionX() + (camera.GetWorldWidth() / 2.0f);

    public static float GetTopEdge(this Camera camera) => camera.GetPositionY() + camera.orthographicSize;

    public static float GetBottomEdge(this Camera camera) => camera.GetPositionY() - camera.orthographicSize;

    public static bool IsBeyondLeftEdge(this Camera camera, float positionX, float offset = 0.0f) => positionX < camera.GetLeftEdge() - offset;

    public static bool IsBeyondRightEdge(this Camera camera, float positionX, float offset = 0.0f) => positionX > camera.GetRightEdge() + offset;

    public static bool IsAboveTopEdge(this Camera camera, float positionY, float offset = 0.0f) => positionY > camera.GetTopEdge() + offset;

    public static bool IsBelowBottomEdge(this Camera camera, float positionY, float offset = 0.0f) => positionY < camera.GetBottomEdge() - offset;
}