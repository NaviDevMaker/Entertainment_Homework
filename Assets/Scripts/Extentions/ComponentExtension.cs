using UnityEngine;


namespace Game.Extension
{
    public static class ComponentExtension
    {
        public static bool TryGetComponentInChildren<TCmp>(this Component cmp, out TCmp targetCmp) where TCmp : Component        
        {
            return cmp.gameObject.TryGetComponentInChildren<TCmp>(out targetCmp);
        }
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
        public static bool TryGetComponentInParent<TCmp>(this Component cmp, out TCmp targetCmp) where TCmp : Component
        {
            return cmp.gameObject.TryGetComponentInParent<TCmp>(out targetCmp);
        }
    }
}


