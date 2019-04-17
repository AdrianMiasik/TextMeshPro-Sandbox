using TMPro;

namespace Text.Reveals
{
	public class Character
	{
		public float timeSinceReveal;
		public bool IsRevealed = false;
		private TMP_CharacterInfo _info;

		public Character(TMP_CharacterInfo info)
		{
			_info = info;
		}

		public TMP_CharacterInfo Info()
		{
			return _info;
		}
	}
}