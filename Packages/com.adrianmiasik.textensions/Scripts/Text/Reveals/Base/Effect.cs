using Text.Reveals.Base;
using UnityEngine;

namespace Text.Reveals
{
	public abstract class Effect : ScriptableObject
	{
		public abstract float Calculate(Character character);
	}
}