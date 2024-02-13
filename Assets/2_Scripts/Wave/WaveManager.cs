using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoSingleton<WaveManager>
{
    [field: SerializeField] public int Wave { get; private set; }

    public UnityAction<int> WaveTextAction;

    public int IncreaseWave()
    {
        Wave++;
        WaveTextAction?.Invoke(Wave);
        return Wave;
    }
}
