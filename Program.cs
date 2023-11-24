using System;

public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

public class PropertyEventArgs : EventArgs
{
    public string PropertyName { get; }

    public PropertyEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }
}

public interface IPropertyChanged
{
    event PropertyEventHandler PropertyChanged;
}

public class MyClass : IPropertyChanged
{
    private string _name;

    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public event PropertyEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyEventArgs(propertyName));
    }
}

class Program
{
    static void Main()
    {
        MyClass obj = new MyClass();

        // Подписываемся на событие изменения свойства
        obj.PropertyChanged += PropertyChangeHandler;

        // Изменяем свойство, чтобы вызвать событие
        obj.Name = "New Name";

        Console.ReadLine();
    }

    static void PropertyChangeHandler(object sender, PropertyEventArgs e)
    {
        Console.WriteLine($"Property '{e.PropertyName}' changed");
    }
}
