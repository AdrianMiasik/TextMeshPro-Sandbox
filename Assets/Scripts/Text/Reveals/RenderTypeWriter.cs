// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

using Text.Reveals.Base;
using TMPro;

namespace Text.Reveals
{
	/// <summary>
	/// Reveals text characters over time much like a typewriter (Using the TextMeshProUGUI component).
	/// </summary>
	/// <summary>
	/// Render Reveal: Characters aren't even being displayed by TMP until we tell it to. 
	/// (This means that the mesh for each character isn't being generated/calculated until told)
	/// The way we are revealing each character is by incrementing the maxVisibleCharacters int on the TextMeshProUGUI class.
	/// </summary>
	public class RenderTypeWriter : TextReveal
	{
		/// <summary>
		/// Hides the text by not rendering any of the character
		/// </summary>
		protected override void HideText()
		{
			// Hide the text
			displayText.maxVisibleCharacters = 0;
		}

		protected override void CharacterReveal(int characterIndex)
		{
			// Reveal a character
			displayText.maxVisibleCharacters = characterIndex + 1;
		}

		protected override void EffectsTick()
		{
			// Optional
		}
	}
}