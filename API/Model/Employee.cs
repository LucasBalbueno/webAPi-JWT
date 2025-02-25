namespace API.Model;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; private set; } = string.Empty;
    
    public int Age { get; set; }

    public string Photo { get; set; } = string.Empty;

    public void ChangeName(string name)
    {
        Name = name;
    }
}