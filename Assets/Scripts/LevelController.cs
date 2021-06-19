using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1;
    private static int _numLevels = 3;
    private Enemy[] _enemies;    

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in _enemies)
        {
            if(enemy != null)
            {
                return;
            }
        }

        Debug.Log("You killed all enemies");

        _nextLevelIndex++;

        if (_nextLevelIndex > _numLevels)
        {
            SceneManager.LoadScene("MainMenu");
            _nextLevelIndex = 1;
        }
        else
        {
            string nextLevelName = "Level" + _nextLevelIndex;
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
