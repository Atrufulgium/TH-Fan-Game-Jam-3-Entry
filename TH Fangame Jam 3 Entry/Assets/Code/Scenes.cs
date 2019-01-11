using UnityEngine.SceneManagement;

public static class Scenes {
    // All levels in order by scene name
    // 1-3: Hakurei Shrine
    // 4-6: Forest of Magic
    // 7-9: Youkai Mountain
    public static string[] levels = { "Level 1" , "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9"};

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