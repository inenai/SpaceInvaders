using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu]
    public class EnemyRepository : ScriptableObject
    {
        public EnemyData[] repository;
    }

    [System.Serializable]
    public struct EnemyData
    {
        public Color Color;
        public int Life;
        public int Score;

        public int Kind
        {
            get
            {
                return (Color.GetHashCode().ToString() + Life.ToString() + Score.ToString()).GetHashCode();
            }
        }
    }
}