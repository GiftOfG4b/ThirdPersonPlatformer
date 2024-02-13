using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// BreakableBlocks Script. (only reason different from gate, is in case disabling itself causes issues)
/// </summary>
public class BreakableBlocks : Script
{
    public Actor block;
    private void OnButtonPressed()
    {
        //disable object

        if(block){
            block.IsActive = false;
        }	
    
    }
}
