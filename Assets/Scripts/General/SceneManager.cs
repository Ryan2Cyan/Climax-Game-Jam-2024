namespace General
{
    public static class SceneManager
    {
        public enum Scene
        {
            BootUp = 0,
            MainMenu = 1,
            Game = 2,
        }

        #region PublicFunctions

        public static void ChangeScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }

        #endregion
    }
}
