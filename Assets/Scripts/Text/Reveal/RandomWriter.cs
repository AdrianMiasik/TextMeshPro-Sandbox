using System.Collections;
using System.Collections.Generic;
using Text.Reveal;
using TMPro;
using UnityEngine;

public class RandomWriter : TextReveal
{
	/// <summary>
	/// Hides the text by getting each characters mesh and setting the alpha of each vertex to zero rendering it invisible.
	/// </summary>
	protected override void HideText()
	{
		// Force the mesh update so we don't have to wait a frame to get the data.
		displayText.ForceMeshUpdate();
		
		// TODO: Store this variable somewhere else, since this is a WIP we shall leave it here for now.
		Color32 invisible = displayText.color;
		invisible.a = 10; // 10 percent opacity to see what we are doing
		
		// Iterate through each character
		for (int i = 0; i <= _numberOfCharacters; i++)
		{
			// Fetch character information
			TMP_CharacterInfo characterInformation = displayText.textInfo.characterInfo[i];
			
			// Bottom Left, Top Left, Top Right, Bottom Right
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 0] = invisible;
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 1] = invisible;
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 2] = invisible;
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 3] = invisible;

		}

		// Refresh data to render correctly
		displayText.UpdateVertexData();
	}

	protected override void CharacterReveal()
    {
	    Debug.Log("Character Reveal!");
	    
	    // TODO: Reveal a random character by changing its vertex color from invisible to which ever.
	    // We will need to somehow record which character is already showing. Maybe we can look into:
	    // displayText.textInfo.characterInfo[0].isVisible ?
    }
}
