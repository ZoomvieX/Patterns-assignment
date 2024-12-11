// Базовый интерфейс
public interface ICloneable3D
{
    ICloneable3D Clone();
}

// Базовый класс 3D-объекта
public abstract class Base3DObject : ICloneable3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public string Color { get; set; }

    public abstract ICloneable3D Clone();

    public override string ToString()
    {
        return $"{GetType().Name}: X={X}, Y={Y}, Z={Z}, Color={Color}";
    }
}


// Куб
public class Cube : Base3DObject
{
    public double Size { get; set; }

    public Cube(double x, double y, double z, string color, double size)
    {
        X = x;
        Y = y;
        Z = z;
        Color = color;
        Size = size;
    }

    public override ICloneable3D Clone()
    {
        return new Cube(X, Y, Z, Color, Size);
    }
}

// Сфера
public class Sphere : Base3DObject
{
    public double Radius { get; set; }

    public Sphere(double x, double y, double z, string color, double radius)
    {
        X = x;
        Y = y;
        Z = z;
        Color = color;
        Radius = radius;
    }

    public override ICloneable3D Clone()
    {
        return new Sphere(X, Y, Z, Color, Radius);
    }
}


