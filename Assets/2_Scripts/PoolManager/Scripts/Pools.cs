using System;
using UnityEngine;

namespace TMN.PoolManager
{
    public class Pools : MonoBehaviour
    {
        public enum Types
        {
            Enemy = 0,
            UpgradePlace = 1,
            Weapon = 2,
        }

        public static string GetTypeStr(Types poolType)
        {
            return Enum.GetName(typeof(Types), poolType);
        }
    }   
}
