using TMPro;
using UnityEngine;

namespace Text
{
	public class Character
	{
		// TODO: Don't give ownership of the parent mesh to each character.
		// This will give us headaches in the future. We want one place where 
		// the mesh is being controlled by, not one access point per character.
		private TMP_MeshInfo parentMesh;
		
		private TMP_CharacterInfo characterInfo;
		private int vertexIndex;
		private Color32 cachedColor;
		private Color32 invisible;
		
		private bool IsEffectActive;
		private bool IsRevealed;

		public Character(TMP_MeshInfo mesh, TMP_CharacterInfo charInfo, Color32 currentColor)
		{
			parentMesh = mesh;
			characterInfo = charInfo;
			vertexIndex = characterInfo.vertexIndex;
			cachedColor = currentColor;
			
			invisible = cachedColor;
			invisible.a = 0;
		}

		public void Hide()
		{
			Color(invisible);

			// TODO: Investigate the isVisible variable and see how TMP is using it
			characterInfo.isVisible = false;
			IsRevealed = false;

//			Debug.Log("Hiding character: " + characterInfo.character);

			IsEffectActive = false;
		}
		
		public void Reveal(Color32 color)
		{
			Color(color);
			
			// TODO: Investigate the isVisible variable and see how TMP is using it
			characterInfo.isVisible = true;
			IsRevealed = true;
			
//			Debug.Log("Revealing character: " + characterInfo.character);

			// If our characters effect is not active...
			if (!IsEffectActive)
			{
				// Allow the effect to play
				IsEffectActive = true;
			}
		}

		/// <summary>
		/// Changes the color of this character.
		/// </summary>
		/// <summary>
		/// Note: Colorizes all the vertices on this characters mesh all at once to a certain color.
		/// </summary>
		/// <param name="color"></param>
		private void Color(Color32 color)
		{			
			// Bottom Left
			parentMesh.colors32[vertexIndex + 0] = color;
			
			// Top Left
			parentMesh.colors32[vertexIndex + 1] = color;

			// Top Right
			parentMesh.colors32[vertexIndex + 2] = color;
			
			// Bottom Right
			parentMesh.colors32[vertexIndex + 3] = color;
		}
	}
}