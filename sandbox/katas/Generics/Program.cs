using System.Diagnostics.CodeAnalysis;

IEnumerable<string> strings = ["one", "two", "three"];
IEnumerable<int> numbers = [1, 2, 3];
Console.WriteLine(Max("A", "b"));
Console.WriteLine(Max2(strings));

var stringToInteger = numbers.Select(i => i.ToString());

void WriteToConsole<T>(IEnumerable<T> list) where T : IComparable<T>
{
    foreach (var s in list)
    {
        Console.Write(s + ", ");
        s.CompareTo(s);
    }
    Console.WriteLine();
}

T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

T Max2<T>(IEnumerable<T> values) where T : IComparable<T>
{
    return values.Max();
}

bool IsDistinct<T>(IEnumerable<T> values) where T : IComparable<T>
{
    var seenElements = new HashSet<IComparable<T>>();
    foreach (var value in values)
    {
        if (!seenElements.Add(value))
        {
            return false;
        }
    }
    return true;

}
