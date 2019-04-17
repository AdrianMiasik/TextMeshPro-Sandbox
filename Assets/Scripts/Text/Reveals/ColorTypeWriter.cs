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
	/// Each character mesh has 4 (56) vertices and we are changing the color32 of each corner at once.
	/// </summary>
	public class ColorTypeWriter : TextReveal
	{
		public float scale = 1f;
		public AnimationCurve myScale;

		public float currentTime;

		protected override void RevealCharacter(int index)
		{
			base.RevealCharacter(index);
			
			// Color
			ColorSingleCharacter(allCharacters[index].Info(), CachedColor);
			DisplayText.textInfo.meshInfo[0].mesh.colors32 = DisplayText.textInfo.meshInfo[0].colors32;
		}

		public override void Update()
		{
			base.Update();
						
//			// We have nothing to update, lets stop ticking
//			if (allCharacters.Count <= 0) return;
			
//			currentTime += Time.deltaTime;

//			scale = myScale.Evaluate(currentTime);

			for (int i = 0; i < allCharacters.Count; i++)
			{
				// Cache character
				Character c = allCharacters[i];
				
				// TODO: If the reveal is sequential then break early 
				// If this character is not revealed or not visible, then skip it.
				if (!c.IsRevealed || !c.Info().isVisible) continue;
				
				c.timeSinceReveal += Time.deltaTime;
				
				// If this characters reveal has went over the animation curve in terms of time
				if (c.timeSinceReveal <= myScale[myScale.length - 1].time)
				{
					AddCharacterScale(c.Info(), myScale.Evaluate(c.timeSinceReveal));
				}
				else
				{
					ColorSingleCharacter(c.Info(), Color.green);
				}
			}
			
			ApplyMeshChanges();
			
			// TODO: Complete tick cycle (When the effect is done, don't update for this character)
			
			// Temp: Update colors
			DisplayText.textInfo.meshInfo[0].mesh.colors32 = DisplayText.textInfo.meshInfo[0].colors32;
		}

		protected override void ApplyMeshChanges()
		{
			// TODO: Move color into here?
			
			base.ApplyMeshChanges();
		}

		public override void InitializeScale()
		{
			currentTime = 0;
		}

		protected override void HideAllCharacters()
		{
			base.HideAllCharacters();
			
			ColorAllCharacters(new Color32(0,0,0,0));
		}
		
		/// <summary>
		/// Hides the text by getting every characters mesh and setting the alpha of each vertex to zero rendering it invisible.
		/// </summary>
		/// <summary>
		/// Note: Remember to update your mesh vertex color data. Update the mesh colors & update geometry or use UpdateVertexData() 
		/// </summary>
		protected void ColorAllCharacters(Color32 color)
		{						
			// Iterate through each character
			for (int i = 0; i < DisplayText.textInfo.characterCount; i++)
			{
				// Bottom Left, Top Left, Top Right, Bottom Right
				DisplayText.textInfo.meshInfo[0].colors32[DisplayText.textInfo.characterInfo[i].vertexIndex + 0] = color;
				DisplayText.textInfo.meshInfo[0].colors32[DisplayText.textInfo.characterInfo[i].vertexIndex + 1] = color;
				DisplayText.textInfo.meshInfo[0].colors32[DisplayText.textInfo.characterInfo[i].vertexIndex + 2] = color;
				DisplayText.textInfo.meshInfo[0].colors32[DisplayText.textInfo.characterInfo[i].vertexIndex + 3] = color;
			}

			for (int i = 0; i < DisplayText.textInfo.meshInfo.Length; i++)
			{
				DisplayText.textInfo.meshInfo[i].mesh.colors32 = DisplayText.textInfo.meshInfo[i].colors32;
				DisplayText.UpdateGeometry(DisplayText.textInfo.meshInfo[i].mesh, i);
			}
		}

		/// <summary>
		/// Changes the color of a single character.
		/// </summary>
		/// <summary>
		/// Colorizes all the vertices on a characters mesh all at once to a certain color.
		/// Note: Remember to update your mesh vertex color data. Update the mesh colors & update geometry or use UpdateVertexData() 
		/// </summary>
		/// <param name="character"></param>
		/// <param name="color"></param>
		protected void ColorSingleCharacter(TMP_CharacterInfo character, Color32 color)
		{
			// Bottom Left, Top Left, Top Right, Bottom Right
			DisplayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 0] = color;
			DisplayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 1] = color;
			DisplayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 2] = color;
			DisplayText.textInfo.meshInfo[0].colors32[character.vertexIndex + 3] = color;
		}
	}
}
