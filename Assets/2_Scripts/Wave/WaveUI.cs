using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    private TextMeshProUGUI _waveTxt;

    private void Start()
    {
        _waveTxt = GetComponent<TextMeshProUGUI>();
        WaveManager.Instance.WaveTextAction = SetWaveText;
    }

    public void SetWaveText(int wave)
    {
        _waveTxt.text = "Wave: " + wave;
    }
}