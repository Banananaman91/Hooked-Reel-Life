using System.Collections;
using System.Collections.Generic;
using TurnBasedAssets.Scripts.Characters.PlayerControls;
using TurnBasedAssets.Scripts.Characters.NPCControls;
using TurnBasedAssets.Scripts.MouseController;
using UnityEngine;

namespace TurnBasedAssets.Scripts.Characters
{
    public class MoveManager : MonoBehaviour //idea 1) this could instead be a function for Controller
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<NPCController> npcControllers;
        [SerializeField] private MouseSelection mouseSelection;
        // create enums

        public void StartCharacterMoves(bool isPlayersTurn)
        {
            switch (isPlayersTurn)
            {
                case true:
                    //makes MouseSelection usuable, in turn making player controls available.
                    //StartCoroutine(playerController.FindPossibleMovePositions(mouseSelection.RawGridPoint)); // at the end of players movement, switch enum on Controller to NPC moving
                    break;

                default: //case NPC moving, start coroutine for npcs to start finding possible move positions, which ends starting movement
                    break;

                    //it could be possible (emphasis on possible, it is just an idea) to use the switch case individually.
                    //If on Controller, those that inherit Controller can have their enum predetermined
                    //that way all NPC's with an NPC enum COULD move simultaneously, if I'm correct, without needing a list of NPC's
            }
        }
    }
}
