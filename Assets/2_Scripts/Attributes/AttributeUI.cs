using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour
{
    [SerializeField] private Image healthBar; 

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetHealthBar(float value, int max)
    {
        if (value == max || max == 0 || value <= 0)
            _canvasGroup.alpha = 0;
        else
        {
            _canvasGroup.alpha = 1;
            healthBar.transform.localScale = new Vector3(value / max, 1, 1);
        }
    }
}