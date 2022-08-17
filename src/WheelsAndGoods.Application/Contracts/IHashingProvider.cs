namespace WheelsAndGoods.Application.Contracts;

	public interface IHashingProvider
	{
		string GetHash(string value);
		bool Verify(string value, string hash);
	}
