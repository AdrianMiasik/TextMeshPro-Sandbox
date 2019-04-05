// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

using UnityEngine;
using TMPro;
using UnityEngine.Profiling;

namespace Text.Reveal
{
	/// <summary>
	/// Reveals text characters over time much like a typewriter (Using the TextMeshProUGUI component).
	/// </summary>
	/// <summary>
	/// There is a delay in-between each character reveal.
	/// </summary>
	public class TypeWriter : TextReveal
	{
		/// <summary>
		/// Hides the text by not rendering any of the character
		/// </summary>
		protected override void HideText()
		{
			// Hide the text
			displayText.maxVisibleCharacters = 0;
		}

		protected override void CharacterReveal()
		{
			// Reveal a character
			displayText.maxVisibleCharacters = _numberOfCharactersRevealed;
		}
	}
}