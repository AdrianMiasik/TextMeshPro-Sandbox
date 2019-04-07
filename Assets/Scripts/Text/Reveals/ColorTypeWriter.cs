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
		protected List<TMP_CharacterInfo> _characters = new List<TMP_CharacterInfo>();
		private Color32 _cachedColor;
	
		/// <summary>
		/// Hides the text by getting each characters mesh and setting the alpha of each vertex to zero rendering it invisible.
		/// </summary>
		protected override void HideText()
		{
			// Force the mesh update so we don't have to wait a frame to get the data.
			displayText.ForceMeshUpdate();

			_cachedColor = displayText.color;
		
			// TODO: Store this variable somewhere else, since this is a WIP we shall leave it here for now.
			Color32 invisible = displayText.color;
			invisible.a = 0;
		
			// Iterate through each character
			for (var i = 0; i <= NumberOfCharacters - 1; i++)
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

			// TODO: When we are hiding our text, we shouldn't need to always need to fetch our characters unless we are definitely going to reveal later.
			GetCharacters();
		}

		protected virtual void GetCharacters()
		{
			_characters = FetchInvisibleCharacters(displayText);
		}

		protected override void CharacterReveal()
		{	    
			if (_characters.Count > 0)
			{   
				// Bottom Left, Top Left, Top Right, Bottom Right
				displayText.textInfo.meshInfo[0].colors32[_characters[0].vertexIndex + 0] = _cachedColor;
				displayText.textInfo.meshInfo[0].colors32[_characters[0].vertexIndex + 1] = _cachedColor;
				displayText.textInfo.meshInfo[0].colors32[_characters[0].vertexIndex + 2] = _cachedColor;
				displayText.textInfo.meshInfo[0].colors32[_characters[0].vertexIndex + 3] = _cachedColor;

				// Mark this character as visible
				// TODO: Investigate the isVisible variable and see how TMP is using it
				displayText.textInfo.characterInfo[0].isVisible = true;

				_characters.RemoveAt(0);

				// Refresh data to render correctly
				displayText.UpdateVertexData();
			}
		}

		/// <summary>
		/// Returns a list of characters that aren't visible in the provided text.
		/// </summary>
		private List<TMP_CharacterInfo> FetchInvisibleCharacters(TMP_Text text)
		{
			List<TMP_CharacterInfo> invisibleCharacters = new List<TMP_CharacterInfo>();

			// Iterate through each character
			for (var i = 0; i <= text.textInfo.characterCount - 1; i++)
			{
				// Get each character that isn't visible
				if (!text.textInfo.characterInfo[i].isVisible)
				{
					invisibleCharacters.Add(text.textInfo.characterInfo[i]);
				}
			}
			
			return invisibleCharacters;
		}
	}
}
