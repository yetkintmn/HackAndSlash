using UnityEngine.SceneManagement;

public class PlayerAttribute : AttributeBase
{
    private void Start()
    {
        LoadAttribute();
    }

    public void Heal(int value)
    {
        HealthRestore(value);
    }

    protected override void Die()
    {
        base.Die();
        SceneManager.LoadScene(0);
    }
}