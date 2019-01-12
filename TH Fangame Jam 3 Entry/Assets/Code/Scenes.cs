using UnityEngine.SceneManagement;

public static class Scenes {
    // All levels in order by scene name
    // 1-3: Hakurei Shrine
    // 4-6: Forest of Magic
    // 7-9: Youkai Mountain
    // also isn't this beautifully the shittiest way to handle this? I should be proud. I can't think of anything worse even if I try.
    public static string[] levels = { "Main Menu", "Level 1" , "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9", "Finish Screen"};
    public static int currentLevel = 0;

    // Only really relevant when starting levels from the editor
    public static void CheckLevel() {
        string level = SceneManager.GetActiveScene().name;
        for (int i = 0; i < levels.Length; i++) {
            if (levels[i] == level) {
                currentLevel = i;
                break;
            }
        }
    }

    public static void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadLevel(string level) {
        SceneManager.LoadScene(level);
        CheckLevel();
    }

    public static void LoadNextLevel() {
        //wasteful but who cares, not called often, not a large array
        LoadLevel(levels[currentLevel+1]);
    }
}