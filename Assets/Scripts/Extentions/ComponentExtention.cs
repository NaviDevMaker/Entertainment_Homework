using UnityEngine;


namespace Game.Extention
{
    public static class ComponentExtention
    {
        public static bool TryGetComponentInChildren<TCmp>(this GameObject gameObject, out TCmp cmp) where TCmp : Component        
        {
            cmp = gameObject.GetComponentInChildren<TCmp>();
            return cmp != null;
        }
        public static bool TryGetComponentInParent<TCmp>(this GameObject gameObject, out TCmp cmp) where TCmp : Component
        {
            cmp = gameObject.GetComponentInParent<TCmp>();
            return cmp != null;
        }
    }
}


