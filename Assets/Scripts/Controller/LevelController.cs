using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int level;

    private void Awake()
    {
        level = 0;
    }

    public void LoadNextLevel()
    {
        level++;
        switch (level)
        {
            case 1:
                gameObject.AddComponent<Level1>();
                break;
        }
    }
}
