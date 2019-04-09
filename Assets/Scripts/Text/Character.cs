using TMPro;
using UnityEngine;

namespace Text
{
	public class Character
	{
		// TODO: Don't give ownership of the parent mesh to each character.
		// This will give us headaches in the future. We want one place where 
		// the mesh is being controlled by, not one access point per character.
		private readonly TMP_MeshInfo _parentMesh;
		
		private TMP_CharacterInfo _characterInfo;
		private readonly int _vertexIndex;
		private readonly Color32 _invisible;
		
		public bool IsEffectActive;
		public bool IsRevealed;

		public Character(TMP_TextInfo meshInfo, int characterIndex, Color32 currentColor)
		{
			_parentMesh = meshInfo.meshInfo[0];
			_characterInfo = meshInfo.characterInfo[characterIndex];
			_vertexIndex = _characterInfo.vertexIndex;

			_invisible = currentColor;
			_invisible.a = 0;
		}

		public void Hide()
		{
			Color(_invisible);

			// TODO: Investigate the isVisible variable and see how TMP is using it
			_characterInfo.isVisible = false;
			IsRevealed = false;

//			Debug.Log("Hiding character: " + characterInfo.character);
		}
		
		public void Show(Color32 color)
		{
			Color(color);
			
			// TODO: Investigate the isVisible variable and see how TMP is using it
			_characterInfo.isVisible = true;
			IsRevealed = true;
			
//			Debug.Log("Revealing character: " + characterInfo.character);
		}

		public void Scale(TMP_MeshInfo[] vertInfo, TMP_TextInfo textInfo, float scale)
		{
			int materialIndex = _characterInfo.materialReferenceIndex;
			Vector3[] originalVertices = vertInfo[materialIndex].vertices;
			
			// (Bottom left + top right) divided by 2 = center of mesh
			Vector3 characterOrigin = (originalVertices[_vertexIndex + 0] + originalVertices[_vertexIndex + 2]) / 2;
			
			Vector3[] targetVertices = textInfo.meshInfo[materialIndex].vertices;

			targetVertices[_vertexIndex + 0] = originalVertices[_vertexIndex + 0] - characterOrigin;
			targetVertices[_vertexIndex + 1] = originalVertices[_vertexIndex + 1] - characterOrigin;
			targetVertices[_vertexIndex + 2] = originalVertices[_vertexIndex + 2] - characterOrigin;
			targetVertices[_vertexIndex + 3] = originalVertices[_vertexIndex + 3] - characterOrigin;

			Matrix4x4 matrix = Matrix4x4.Scale(Vector3.one * scale);

			targetVertices[_vertexIndex + 0] = matrix.MultiplyPoint3x4(targetVertices[_vertexIndex + 0]);
			targetVertices[_vertexIndex + 1] = matrix.MultiplyPoint3x4(targetVertices[_vertexIndex + 1]);
			targetVertices[_vertexIndex + 2] = matrix.MultiplyPoint3x4(targetVertices[_vertexIndex + 2]);
			targetVertices[_vertexIndex + 3] = matrix.MultiplyPoint3x4(targetVertices[_vertexIndex + 3]);

			targetVertices[_vertexIndex + 1] += characterOrigin;
			targetVertices[_vertexIndex + 2] += characterOrigin;
			targetVertices[_vertexIndex + 0] += characterOrigin;
			targetVertices[_vertexIndex + 3] += characterOrigin;
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
			_parentMesh.colors32[_vertexIndex + 0] = color;
			
			// Top Left
			_parentMesh.colors32[_vertexIndex + 1] = color;

			// Top Right
			_parentMesh.colors32[_vertexIndex + 2] = color;
			
			// Bottom Right
			_parentMesh.colors32[_vertexIndex + 3] = color;
		}
	}
}