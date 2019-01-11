using UnityEngine.SceneManagement;

public static class Scenes {
    // All levels in order by scene name
    public static string[] levels = { "Level 1" , "Level 2", " Level 3"};

    public static void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadLevel(int levelID) {
        if (levelID <= 0 || levelID > levels.Length)
            throw new System.IndexOutOfRangeException("Reminder: levels are 1-indexed");
        SceneManager.LoadScene($"Level {levelID}");
    }

    public static void LoadNextLevel() {
        //wasteful but who cares, not called often, not a large array
        string currentLevel = SceneManager.GetActiveScene().name;
        int i = 0;
        for (; i < levels.Length; i++) {
            if (currentLevel == levels[i])
                break;
        }
        LoadLevel(i + 2); // +1 because 1-indexed, +1 because "next"
    }
}