public interface IAtivavel
{
    string Name { get; }
    bool isActive { get; set; }

    void Activate(bool ativo);
}
