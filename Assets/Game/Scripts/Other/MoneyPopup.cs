using TMPro;
using UnityEngine;

public class MoneyPopup : MonoBehaviour
{
    public TMP_Text text;

    public float moveDistance = 30f;
    public float duration = 0.7f;

    private Vector3 startPosition;
    private float timer = 0f;

    public void Setup(int amount)
    {
        text.text = $"+${amount}";

        startPosition = transform.localPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float progress = timer / duration;

        transform.localPosition = startPosition + Vector3.up * (moveDistance * progress);

        Color color = text.color;
        color.a = 1f - progress;
        text.color = color;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
