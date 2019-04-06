﻿using System.Collections.Generic;
using Text.Reveal;
using TMPro;
using UnityEngine;
using static Text.Tools.ListUtilities;

 public class RandomWriter : TextReveal
{
	private bool hasRandomOrder = false;
	private List<TMP_CharacterInfo> characters = new List<TMP_CharacterInfo>();

	private Color32 cachedColor;
	
	/// <summary>
	/// Hides the text by getting each characters mesh and setting the alpha of each vertex to zero rendering it invisible.
	/// </summary>
	protected override void HideText()
	{
		// Force the mesh update so we don't have to wait a frame to get the data.
		displayText.ForceMeshUpdate();

		cachedColor = displayText.color;
		
		// TODO: Store this variable somewhere else, since this is a WIP we shall leave it here for now.
		Color32 invisible = displayText.color;
		invisible.a = 0;
		
		// Iterate through each character
		for (int i = 0; i <= _numberOfCharacters - 1; i++)
		{
			// Fetch character information
			TMP_CharacterInfo characterInformation = displayText.textInfo.characterInfo[i];
			
			// Bottom Left, Top Left, Top Right, Bottom Right
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 0] = invisible;
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 1] = invisible;
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 2] = invisible;
			displayText.textInfo.meshInfo[0].colors32[characterInformation.vertexIndex + 3] = invisible;
			
			// Mark each character as not visible
			displayText.textInfo.characterInfo[i].isVisible = false;
		}

		// Refresh data to render correctly
		displayText.UpdateVertexData();

		hasRandomOrder = false;
		characters.Clear();
	}

	protected override void CharacterReveal()
    {
	    // If we don't have a random order to reveal...
	    if (!hasRandomOrder)
	    {
		    FetchRandomOrder();
		    hasRandomOrder = true;
	    }
	    
	    if (characters.Count > 0)
	    {   
		    // Bottom Left, Top Left, Top Right, Bottom Right
		    displayText.textInfo.meshInfo[0].colors32[characters[0].vertexIndex + 0] = cachedColor;
		    displayText.textInfo.meshInfo[0].colors32[characters[0].vertexIndex + 1] = cachedColor;
		    displayText.textInfo.meshInfo[0].colors32[characters[0].vertexIndex + 2] = cachedColor;
		    displayText.textInfo.meshInfo[0].colors32[characters[0].vertexIndex + 3] = cachedColor;

		    // Mark this character as visible (TODO: Investigate the isVisible variable and see how TMP is using it)
		    displayText.textInfo.characterInfo[0].isVisible = true;

		    characters.RemoveAt(0);

		    // Refresh data to render correctly
		    displayText.UpdateVertexData();
	    }
    }

	/// <summary>
	/// Get each character that isn't visible and 
	/// </summary>
	private void FetchRandomOrder()
	{		
		// Iterate through each character
		for (int i = 0; i <= _numberOfCharacters - 1; i++)
		{
			// Get each character that isn't visible
			if (!displayText.textInfo.characterInfo[i].isVisible)
			{
				characters.Add(displayText.textInfo.characterInfo[i]);
			}
		}

		Shuffle(characters);

//		// Print each character
//		for (int i = 0; i < characters.Count; i++)
//		{
//			Debug.Log(characters[i].character);
//		}
	}
}
