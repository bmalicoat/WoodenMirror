using UnityEngine;
using System.Collections;
using System;

public class Block : MonoBehaviour
{
    // between 255 - 0
    public float CurrentValue = 127.5f;
    public float TargetValue = 127.5f;

    private float RotationSpeed = 40.0f;
    private float Epsilon = 5.0f;

    private float MinValue = 0.0f;
    private float MaxValue = 255.0f;

    private float MinAngleInDegrees = -45.0f;
    private float MaxAngleInDegrees = 45.0f;

    void Update()
    {
        // still has a bug that the step can go passed the target...
        if (Mathf.Abs(CurrentValue - TargetValue) < Epsilon)
        {
            CurrentValue = TargetValue;

            transform.localRotation = Quaternion.Euler(
                transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y,
                ValueToDeg(CurrentValue));

            return;
        }

        float startAngleInDegrees = ValueToDeg(DegToValue(transform.localRotation.eulerAngles.z));
        float deltaZAngleInDegrees = Time.deltaTime * RotationSpeed;
        float newZRotation = startAngleInDegrees;

        newZRotation = newZRotation + (TargetValue < CurrentValue ? -deltaZAngleInDegrees : deltaZAngleInDegrees);

        CurrentValue = DegToValue(newZRotation);

        transform.localRotation = Quaternion.Euler(
            transform.localRotation.eulerAngles.x,
            transform.localRotation.eulerAngles.y,
            newZRotation);
    }

    private float ValueToDeg(float value)
    {
        return (value - (MaxValue / 2.0f)) / (MaxValue / 2.0f) * MaxAngleInDegrees;
    }

    private float DegToValue(float deg)
    {
        if (deg >= (360 + MinAngleInDegrees))
        {
            deg -= 360;
        }

        return deg * (MaxValue / 2.0f) / MaxAngleInDegrees + (MaxValue / 2.0f);
    }

    public void SetValue(float newValue)
    {
        TargetValue = Mathf.Max(MinValue, Mathf.Min(MaxValue, newValue));
    }
}
