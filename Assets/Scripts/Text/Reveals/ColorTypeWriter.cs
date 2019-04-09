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
		private Color32 _cachedColor;
			
		/// <summary>
		/// Hides the text by getting each characters mesh and setting the alpha of each vertex to zero rendering it invisible.
		/// </summary>
		protected override void HideText()
		{
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
				_characters.Add(new Character(displayText.textInfo, i, _cachedColor));
			}
		}

		protected override void CharacterReveal(int characterIndex)
		{	    			
			// Tell the character at a certain index to reveal itself.
			_characters[characterIndex].Show(_cachedColor);
							
			// Refresh data to render correctly
			displayText.UpdateVertexData();
		}

		protected override void EffectsTick()
		{
			for (int i = 0; i < 3; i++)
			{
				_characters[i].Scale(cachedVertexData, displayText.textInfo, 1.5f);
			}

			for (int i = 0; i < displayText.textInfo.meshInfo.Length; i++)
			{
				displayText.textInfo.meshInfo[i].mesh.vertices = displayText.textInfo.meshInfo[i].vertices;
				displayText.textInfo.meshInfo[i].mesh.uv = displayText.textInfo.meshInfo[i].uvs0;
				
				displayText.UpdateGeometry(displayText.textInfo.meshInfo[i].mesh, i);
			}
		}
	}
}
