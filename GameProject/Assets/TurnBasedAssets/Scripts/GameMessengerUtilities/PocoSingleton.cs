namespace TurnBasedAssets.Scripts.GameMessengerUtilities
{
    public abstract class PocoSingleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance => _instance ?? (_instance = new T());
    }
}
