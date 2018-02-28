public interface IActivatable
{
    string Name { get; }
    bool IsActive { get; set; }

    void Activate(bool active);
}
