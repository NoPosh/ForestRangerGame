using System;
using UnityEngine;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class SkillCheckUI : MonoBehaviour
{
    [Header("Ссылки на UI")]
    public RectTransform marker;
    public RectTransform successZoneParent;
    public Image successZoneImage;

    [Header("Настройки")]
    public float baseRotationSpeed = 150f;
    public float zoneWindowDegrees = 45f;

    private float currentSpeed;
    private bool isActive = false;
    private Action<bool> onComplete;
    private float targetAngle;
    public bool IsActive => isActive;

    public void StartCheck(float stressMultiplier, Action<bool> callback)
    {
        onComplete = callback;
        currentSpeed = baseRotationSpeed * stressMultiplier;
        targetAngle = UnityEngine.Random.Range(0f, 360f);
        successZoneParent.localRotation = Quaternion.Euler(0, 0, -targetAngle);
        if (successZoneImage != null)
        {
            successZoneImage.fillAmount = zoneWindowDegrees / 360f;
            successZoneImage.rectTransform.localRotation = Quaternion.Euler(0, 0, zoneWindowDegrees / 2f);
        }
        marker.localRotation = Quaternion.identity;
        isActive = true;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isActive) return;
        marker.Rotate(Vector3.forward, -currentSpeed * Time.deltaTime);
        if (GetActionPressed())
        {
            bool isSuccess = CheckSuccess();
            Resolve(isSuccess);
        }
    }

    private bool GetActionPressed()
    {
#if ENABLE_INPUT_SYSTEM
        bool space = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        bool pointer = Pointer.current != null && Pointer.current.press.wasPressedThisFrame;
        return space || pointer;
#else
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
#endif
    }

    private bool CheckSuccess()
    {
        float currentAngle = (360 - marker.localEulerAngles.z) % 360;
        float diff = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle));
        return diff < (zoneWindowDegrees / 2f);
    }

    private void Resolve(bool success)
    {
        isActive = false;
        gameObject.SetActive(false);
        onComplete?.Invoke(success);
    }
}
