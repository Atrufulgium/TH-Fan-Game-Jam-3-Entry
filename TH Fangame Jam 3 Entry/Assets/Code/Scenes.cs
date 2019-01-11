using UnityEngine.SceneManagement;

public static class Scenes {
    // All levels in order by scene name
    public static string[] levels = { "Level 1" , "Level 2", "Level 3", "Level 4"};

    public static void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public static void LoadNextLevel() {
        //wasteful but who cares, not called often, not a large array
        string currentLevel = SceneManager.GetActiveScene().name;
        int i = 0;
        for (; i < levels.Length; i++) {
            if (currentLevel == levels[i])
                break;
        }
        LoadLevel(levels[i+1]);
    }
}