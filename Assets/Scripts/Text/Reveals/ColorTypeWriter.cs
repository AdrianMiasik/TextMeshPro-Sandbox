using System.Collections.Generic;
using Text.Reveals.Base;
using TMPro;
using UnityEngine;

namespace Text.Reveals
{
	/// <summary>
	/// Reveals text characters over time in a random sequence by changing each characters vertex colors. (Using the TextMeshProUGUI component).
	/// </summary>
	/// <summary>
	/// Color Reveal: Characters are being rendered by TMP but are hidden by changing the alpha of each character mesh to zero.
	/// The way we are revealing each character is by altering the color of the vertices on the characters mesh.
	/// Each character mesh has 4 vertices and we are changing the color32 of each corner at once.
	/// </summary>
	public class ColorTypeWriter : TextReveal
	{
		protected List<Character> _characters = new List<Character>();
		private Color32 _cachedColor;
	
		/// <summary>
		/// Hides the text by getting each characters mesh and setting the alpha of each vertex to zero rendering it invisible.
		/// </summary>
		protected override void HideText()
		{
			// Force the mesh update so we don't have to wait a frame to get the data.
			// Since we need to get information from the mesh we will have to update the mesh a bit earlier than normal.
			// "TMP generates/processes the mesh once per frame (if needed) just before Unity renders the frame."
			// Source: https://www.youtube.com/watch?v=ZHU3AcyDKik&feature=youtu.be&t=164
			// In most cases it's fine for TMP to render at it's normal timings but as mentioned above if we are going
			// to manipulate or fetch data from the mesh we should force the mesh to update so the data remains accurate.
			displayText.ForceMeshUpdate();

			_cachedColor = displayText.color;
			
			// TODO: When we are hiding our text, we shouldn't need to always need to fetch our characters unless we are definitely going to reveal later.
			GetCharacters();
		
			// Iterate through each character
			for (var i = 0; i < displayText.textInfo.characterCount; i++)
			{
				_characters[i].Hide();
			}

			// Refresh data to render correctly
			displayText.UpdateVertexData();
		}

		protected virtual void GetCharacters()
		{
			_characters.Clear();
			
			// Iterate through each character
			for (var i = 0; i < displayText.textInfo.characterCount; i++)
			{				
				// Create a character class for each character
				_characters.Add(new Character(displayText.textInfo.meshInfo[0], displayText.textInfo.characterInfo[i], _cachedColor));
			}
		}

		protected override void CharacterReveal(int characterIndex)
		{	    			
			// Tell the character at a certain index to reveal itself.
			_characters[characterIndex].Reveal(_cachedColor);
							
			// Refresh data to render correctly
			displayText.UpdateVertexData();
		}

		protected override void EffectsTick()
		{
			
		}
	}
}
