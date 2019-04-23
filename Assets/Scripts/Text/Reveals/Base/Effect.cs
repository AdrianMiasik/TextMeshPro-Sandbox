using Text.Reveals.Base;
using UnityEngine;

namespace Text.Reveals
{
	public abstract class Effect : ScriptableObject
	{
		public abstract void Tick(TextReveal txt, Character[] chars);
	}
}