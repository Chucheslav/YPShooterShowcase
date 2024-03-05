using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform bar;

    public void SetHealthBar(float fraction)
    {
        bar.transform.localScale = new Vector3(fraction, bar.transform.localScale.y, bar.transform.localScale.z);
    }
}
