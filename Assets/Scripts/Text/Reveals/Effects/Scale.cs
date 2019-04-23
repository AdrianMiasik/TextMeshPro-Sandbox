using Text.Reveals.Base;
using UnityEngine;

namespace Text.Reveals.Effects
{
	[CreateAssetMenu(menuName = "Text Reveals/Effects/Scale", fileName = "New Scale Effect")]
	public class Scale: Effect
	{
		public AnimationCurve uniform;

		public override void Tick(TextReveal txt, Character[] chars)
		{
			// Iterate through each character in our reveal...
			for (int i = 0; i < chars.Length; i++)
			{
				// Get the current character
				Character c = chars[i];

				// TODO: If the reveal is sequential then break early 
				// If our current character is not revealed or not visible, then skip it.
				if (!c.IsRevealed || !c.Info().isVisible) continue;

				// Accumulate time to this specific character
				c.timeSinceReveal += Time.deltaTime;

				if (uniform.postWrapMode == WrapMode.PingPong || uniform.postWrapMode == WrapMode.Loop)
				{
					txt.AddCharacterScale(c.Info(), uniform.Evaluate(c.timeSinceReveal));
				}
				else
				{
					// If this characters reveal is in progress...
					if (c.timeSinceReveal <= uniform[uniform.length - 1].time)
					{
						// Scale the character
						txt.AddCharacterScale(c.Info(), uniform.Evaluate(c.timeSinceReveal));
					}
				}
			}
		}
	}
}