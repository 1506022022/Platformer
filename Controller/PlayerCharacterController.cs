using RPG.Character;
using RPG.Input;
using UnityEngine;
using static RPG.Input.ActionKey;

namespace Platformer.Core
{
    public static class PlayerCharacterController
    {
        public static void ControllTo(PlayerCharacter character)
        {
            var map = ActionKey.GetAxisRawMap();
            Vector3 moveDir = Vector3.zero;
            moveDir.x += map[HORIZONTAL];
            moveDir.z += map[VERTICAL];
            if(moveDir!=Vector3.zero)
            {
                character.MoveDir(moveDir);
            }

            if(map[JUMP] != 0)
            {
                character.Jump();
            }

            if (map[GUARD] != 0)
            {
                character.Combat(4290000001);
            }
        }

    }
}


