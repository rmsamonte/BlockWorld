using UnityEngine;
using System.Collections;
using Game.Scripts;
using Game.Scripts.UI;
using Game.Scripts.Audio;

// inherit from this class if you want to use the default events as well as custom ones

namespace Uniblocks 
{

    public class DefaultVoxelEvents : VoxelEvents 
    {
        public override void OnMouseDown ( int mouseButton, VoxelInfo voxelInfo ) 
        {
            VoxelInfo newInfo = new VoxelInfo(voxelInfo.adjacentIndex, voxelInfo.chunk); // use adjacentIndex to place the block
            var absoluteBlockPos = newInfo.chunk.VoxelIndexToPosition(newInfo.index);

		    if ( mouseButton == 0 ) 
            { // destroy a block with LMB
                Debug.Log("absoluteBlockPos Y:" + absoluteBlockPos.y);

                if (absoluteBlockPos.y == Constants.Game.BOTTOM_BLOCK_Y)
                {
                    return;
                }

			    Voxel.DestroyBlock (voxelInfo); 
		    }
		    else if ( mouseButton == 1 ) 
            {
                // place a block with RMB
                var playerObject = GameObject.Find(Constants.Game.SINGLE_PLAYER_NAME);

                if (playerObject != null)
                {
                    var playerFeet = playerObject.transform.FindChild(Constants.Game.SINGLE_PLAYER_FEET_NAME);                    

                    if (playerFeet != null)
                    {
                        var playerVoxelInfo = Engine.PositionToVoxelInfo(playerFeet.transform.position);

                        if( playerVoxelInfo == null )
                        {
                            //NOTE: Doesn't build block because it looks like we've reach the top of the world...
                            Debug.Log("Reached top of the world...");
                            return;
                        }

                        var absolutePlayerPos = playerVoxelInfo.chunk.VoxelIndexToPosition(playerVoxelInfo.index);
                        
                        var distance = DistanceFromBlock(absolutePlayerPos, absoluteBlockPos);

                        Debug.Log("Distance: " + distance);

                        if(absolutePlayerPos.x == absoluteBlockPos.x &&
                            absolutePlayerPos.y == (absoluteBlockPos.y-1) &&
                            absolutePlayerPos.z == absoluteBlockPos.z || distance <= Constants.Game.MIN_BLOCK_DISTANCE)
                        {
                            return;
                        }
                    }
                }                

                if (voxelInfo.GetVoxel() == 8)
                { // if we're looking at a tall grass block, replace it with the held block
                    Voxel.PlaceBlock(voxelInfo, ExampleInventory.HeldBlock);
                }
                else
                { // else put the block next to the one we're looking at
                    Voxel.PlaceBlock(newInfo, ExampleInventory.HeldBlock);
                }
		    }		
	    }

        private float DistanceFromBlock(Vector3 player, Vector3 block)
        {
            var dx = player.x - block.x;
            var dy = player.y - block.y;
            var dz = player.z - block.z;

            return Mathf.Sqrt(dx*dx + dy*dy + dz*dz);
        }

	    public override void OnLook ( VoxelInfo voxelInfo ) 
        {
		
		    // move the selected block ui to the block that's being looked at (convert the index of the hit voxel to absolute world position)
		    GameObject blockSelection = GameObject.Find ("selected block graphics");
		    if (blockSelection != null) 
            {
			    blockSelection.transform.position = voxelInfo.chunk.VoxelIndexToPosition (voxelInfo.index);
			    blockSelection.GetComponent<Renderer>().enabled = true;
			    blockSelection.transform.rotation = voxelInfo.chunk.transform.rotation;
		    }
				
	    }

	    public override void OnBlockPlace ( VoxelInfo voxelInfo ) 
        {
            // if the block below is grass, change it to dirt
		    Index indexBelow = new Index (voxelInfo.index.x, voxelInfo.index.y-1, voxelInfo.index.z);	
			
		    if ( voxelInfo.GetVoxelType ().VTransparency == Transparency.solid 
	        && voxelInfo.chunk.GetVoxel(indexBelow) == 2) 
            {	    	    
			    voxelInfo.chunk.SetVoxel(indexBelow, 1, true);
		    }

            PlayBlockBuildSound();
	    }
	
	    public override void OnBlockDestroy ( VoxelInfo voxelInfo ) 
        {
            PlayDestroyBlockSound();

            // if the block above is tall grass, destroy it
		    Index indexAbove = new Index (voxelInfo.index.x, voxelInfo.index.y+1, voxelInfo.index.z);
		
		    if ( voxelInfo.chunk.GetVoxel(indexAbove) == 8 ) 
            {
			    voxelInfo.chunk.SetVoxel(indexAbove, 0, true);
		    }		
	    }

        private void PlayDestroyBlockSound()
        {
            var audioManager = Service.Get<AudioManager>();

            if( audioManager != null )
            {
                audioManager.PlaySound("GAME_DIG");
            }
        }
	
	    public override void OnBlockEnter ( GameObject enteringObject, VoxelInfo voxelInfo ) 
        {	
		    Debug.Log ("OnBlockEnter at " + voxelInfo.chunk.ChunkIndex.ToString() + " / " + voxelInfo.index.ToString());	
	    }

        private void PlayBlockBuildSound()
        {
            var inventorySoundManager = Service.Get<InventorySoundManager>();

            if (inventorySoundManager != null)
            {
                inventorySoundManager.PlayBuildSound();
            }
        }
    }

}

