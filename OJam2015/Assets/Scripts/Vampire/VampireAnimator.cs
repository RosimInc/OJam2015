using UnityEngine;
using System.Collections;

public class VampireAnimator : MonoBehaviour
{
    public Transform Body;
    public Transform Eyebrows;
    public Transform Eyes;
    public Transform ArmLeft;
    public Transform ArmRight;
    public Transform LegLeft;
    public Transform LegRight;
    public GameObject NormalMouth;
    public GameObject HappyMouth;

    private const float SLOPE_VALUE = 10f;
    private const float LEGS_ROTATION_MAX = 28f;
    private const float ARM_ROTATION_MAX = -60f;
    private const float ARM_ROTATION_MIN = 16f;
    private const float EYES_OFFSET = 0.1f;
    private const float IDLE_OFFSET = 0.3f;

    private float legsAngle;
    private float armLeftAngle;
    private float armRightAngle;
    private float idleCounter = 0f;
    private bool _isSmiling = false;

    public void Move(float value)
    {
        MoveIdle(value);
        MoveLegs(value);
        MoveArms(value);
        MoveEyes(value);
    }

    public void Smile()
    {
        if (!_isSmiling)
        {
            StartCoroutine(SmileCoroutine());
        }
    }

    private IEnumerator SmileCoroutine()
    {
        _isSmiling = true;

        float previousEyesYValue = Eyes.localPosition.y;
        float previousEyebrowsYValue = Eyebrows.localPosition.y;

        Eyes.localPosition = new Vector3(0f, 1.47f, 0f);
        Eyebrows.localPosition = new Vector3(0f, 1.73f, 0f);
        NormalMouth.SetActive(false);
        HappyMouth.SetActive(true);

        yield return new WaitForSeconds(1f);

        Eyes.localPosition = new Vector3(0f, previousEyesYValue, 0f);
        Eyebrows.localPosition = new Vector3(0f, previousEyebrowsYValue, 0f);
        NormalMouth.SetActive(true);
        HappyMouth.SetActive(false);

        _isSmiling = false;
    }

    private void MoveIdle(float value)
    {
        float newY;
        idleCounter += Time.deltaTime;

        if (value == 0)
        {
            newY = Mathf.Lerp(-IDLE_OFFSET, IDLE_OFFSET, (Mathf.Sin(idleCounter) + 1) / 2);

            Body.localPosition = new Vector3(Body.localPosition.x, newY, Body.localPosition.z);
        }
        else
        {
            newY = Mathf.Lerp(Body.localPosition.y, 0f, Mathf.SmoothStep(0f, 1f, Time.deltaTime * SLOPE_VALUE));

            Body.localPosition = new Vector3(Body.localPosition.y, newY, Body.localPosition.z);

            idleCounter = 0f;
        }
    }

    private void MoveLegs(float value)
    {
        float direction = value == 0 ? 0 : LEGS_ROTATION_MAX * -value;

        legsAngle = Mathf.Lerp(legsAngle, direction, Mathf.SmoothStep(0f, 1f, Time.deltaTime * SLOPE_VALUE));

        LegLeft.localEulerAngles = new Vector3(LegLeft.localEulerAngles.x, LegLeft.localEulerAngles.y, legsAngle);
        LegRight.localEulerAngles = new Vector3(LegRight.localEulerAngles.x, LegRight.localEulerAngles.y, legsAngle);
    }

    private void MoveArms(float value)
    {
        float directionArmLeft = (value == 0 ? 0 : value > 0f ? ARM_ROTATION_MAX : ARM_ROTATION_MIN) * Mathf.Abs(value);
        float directionArmRight = (value == 0 ? 0 : value > 0f ? -ARM_ROTATION_MIN : -ARM_ROTATION_MAX) * Mathf.Abs(value);

        armLeftAngle = Mathf.Lerp(armLeftAngle, directionArmLeft, Mathf.SmoothStep(0f, 1f, Time.deltaTime * SLOPE_VALUE));
        armRightAngle = Mathf.Lerp(armRightAngle, directionArmRight, Mathf.SmoothStep(0f, 1f, Time.deltaTime * SLOPE_VALUE));

        ArmLeft.localEulerAngles = new Vector3(ArmLeft.localEulerAngles.x, ArmLeft.localEulerAngles.y, armLeftAngle);
        ArmRight.localEulerAngles = new Vector3(ArmRight.localEulerAngles.x, ArmRight.localEulerAngles.y, armRightAngle);
    }

    private void MoveEyes(float value)
    {
        if (_isSmiling) return;

        float newX = Mathf.Lerp(Eyes.localPosition.x, EYES_OFFSET * value, Mathf.SmoothStep(0f, 1f, Time.deltaTime * SLOPE_VALUE));

        Eyes.localPosition = new Vector3(newX, Eyes.localPosition.y, Eyes.localPosition.z);
    }
}
