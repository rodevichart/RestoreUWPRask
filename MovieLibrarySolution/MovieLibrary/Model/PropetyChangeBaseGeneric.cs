namespace MovieLibrary.Model
{
	public class PropetyChangeBaseGeneric<T> : PropetyChangeBase where T : class, new()
	{
		protected T This;

		public static implicit operator T(PropetyChangeBaseGeneric<T> thing) { return thing.This; }

		public PropetyChangeBaseGeneric(T thing = null)
		{
			This = (thing == null) ? new T() : thing;
		}
	}
}