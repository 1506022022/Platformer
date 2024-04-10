using static UnityEngine.Input;
namespace RPG.Input
{
    public static class ActionInput
    {
        public static bool IsAttacking()
        {
            return GetAxisRaw("Fire1") > 0;
        }
        public static bool IsInteraction()
        {
            return GetAxisRaw("Fire2") > 0;
        }
    }
}

