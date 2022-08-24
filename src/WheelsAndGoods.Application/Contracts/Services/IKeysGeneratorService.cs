namespace WheelsAndGoods.Application.Contracts.Services;

public interface IKeysGeneratorService
{
    public string Generate(int length);
    public string GenerateGuidBased();
}
