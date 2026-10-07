using System.Text;

// =====================================================================
//  Session09-Assignment
//  Topics: Primary Constructors, Records, Singleton, var & dynamic,
//          Anonymous Types, Extension Methods, Unit Testing (xUnit)
// =====================================================================

// ---------------------------------------------------------------------
// Part 1 — Primary Constructor & Records  (Q2, Q3, Q4)
// ---------------------------------------------------------------------
Console.WriteLine("===== Part 1: Primary Constructor & Records =====");

// ---- Q2 ----
Patient patient01 = new(1, "Ahmed Ali", "01012345678", "Diabetes");
Patient patient02 = new(1, "Ahmed Ali", "01012345678", "Diabetes");

Console.WriteLine(patient01);
Console.WriteLine(patient02);

// Q2.1
Console.WriteLine($"patient01.GetHashCode() = {patient01.GetHashCode()}");
Console.WriteLine($"patient02.GetHashCode() = {patient02.GetHashCode()}");
Console.WriteLine($"Hash codes equal? {patient01.GetHashCode() == patient02.GetHashCode()}");
// Answer: NOT equal. Patient is a CLASS (reference type). The default GetHashCode()
// is based on the object's reference (its identity in memory), not on its values.
// Two different objects => two different references => two different hash codes,
// even though all the property values are identical.

// Q2.2
Console.WriteLine($"patient01.Equals(patient02) = {patient01.Equals(patient02)}");
// Answer: False. The default Equals() of a class compares REFERENCES (are both variables
// pointing to the same object in the heap?). Here they point to two different objects.

// Q2.3
patient01 = patient02;
Console.WriteLine($"After patient01 = patient02 -> Equals: {patient01.Equals(patient02)}");
Console.WriteLine($"After assignment -> hash codes equal? {patient01.GetHashCode() == patient02.GetHashCode()}");
// What changed: now Equals returns True (and the hash codes are equal) because both variables
// reference the SAME object in memory (reference equality).

// ---- Q3 ----
Console.WriteLine("\n--- PatientDto (record) ---");
PatientDto dto01 = new(1, "Ahmed Ali", "01012345678");
PatientDto dto02 = new(1, "Ahmed Ali", "01012345678");

Console.WriteLine(dto01);
Console.WriteLine($"dto01.GetHashCode() = {dto01.GetHashCode()}");
Console.WriteLine($"dto02.GetHashCode() = {dto02.GetHashCode()}");
Console.WriteLine($"Hash codes equal? {dto01.GetHashCode() == dto02.GetHashCode()}");
Console.WriteLine($"dto01.Equals(dto02) = {dto01.Equals(dto02)}");
dto01 = dto02;
Console.WriteLine($"After dto01 = dto02 -> Equals: {dto01.Equals(dto02)}");
// Difference between class and record:
// - class  => reference equality: two objects with identical values are NOT equal (different hash codes,
//             Equals = false) unless they are the same reference.
// - record => VALUE equality: the compiler generates Equals/GetHashCode/ToString based on the property
//             values, so two records with identical values are equal (same hash code, Equals = true)
//             even though they are two different objects.

// ---- Q4 ----
Console.WriteLine("\n--- PatientMapper ---");
PatientDto mappedDto = PatientMapper.MapFromModelToDto(patient01);
Console.WriteLine(mappedDto);

// ---------------------------------------------------------------------
// Part 2 — Singleton  (Q6)
// ---------------------------------------------------------------------
Console.WriteLine("\n===== Part 2: Singleton =====");

AppLogger logger1 = AppLogger.GetLogger();
AppLogger logger2 = AppLogger.GetLogger();
AppLogger logger3 = AppLogger.GetLogger();
AppLogger logger4 = AppLogger.GetLogger();

Console.WriteLine($"logger1 HashCode = {logger1.GetHashCode()}");
Console.WriteLine($"logger2 HashCode = {logger2.GetHashCode()}");
Console.WriteLine($"logger3 HashCode = {logger3.GetHashCode()}");
Console.WriteLine($"logger4 HashCode = {logger4.GetHashCode()}");
// Observation: all four hash codes are IDENTICAL.
// Why: the constructor is private, so nobody can call "new AppLogger()" from outside. GetLogger()
// creates the instance only the first time (when _instance is null) and afterwards always returns
// the same stored instance. So all four variables reference ONE single object.

// ---------------------------------------------------------------------
// Part 3 — var & dynamic  (Q7)
// ---------------------------------------------------------------------
Console.WriteLine("\n===== Part 3: var & dynamic =====");

// 1. target-typed new
Patient p = new(10, "Mona Hassan", "01155556666", "Asthma");

// 2. var
var patientVar = new Patient(11, "Omar Khaled", "01299998888", "None");

// 3. dynamic
dynamic patientDynamic = new Patient(12, "Laila Samir", "01077774444", "Hypertension");

Console.WriteLine(p);
Console.WriteLine(patientVar);
Console.WriteLine(patientDynamic);

// Difference between var and dynamic:
// - var     => the type is resolved at COMPILE time (the compiler infers it from the right side).
//              It is still strongly typed; it can't change afterwards, and a wrong member name is a compile error.
// - dynamic => the type is resolved at RUN time. The compiler skips type checking, so a wrong member name
//              compiles fine but throws RuntimeBinderException when the code runs. The variable can hold
//              different types during its lifetime.

// ---------------------------------------------------------------------
// Part 4 — Anonymous Types  (Q8)
// ---------------------------------------------------------------------
Console.WriteLine("\n===== Part 4: Anonymous Types =====");

var doctor01 = new { Name = "Sara", Specialty = "Cardiology", ExperienceYears = 8, Salary = 25_000 };
var doctor02 = new { Name = "Sara", Specialty = "Cardiology", ExperienceYears = 8, Salary = 25_000 };

Console.WriteLine($"Name: {doctor01.Name}, Specialty: {doctor01.Specialty}");
Console.WriteLine($"doctor01.GetHashCode() = {doctor01.GetHashCode()}");
Console.WriteLine($"doctor02.GetHashCode() = {doctor02.GetHashCode()}");
Console.WriteLine($"doctor01.GetType() = {doctor01.GetType()}");
Console.WriteLine($"doctor01.Equals(doctor02) = {doctor01.Equals(doctor02)}");
Console.WriteLine($"doctor01.ToString() = {doctor01.ToString()}");
// Comparison of equality:
// - Anonymous type => VALUE equality (like a record): the compiler overrides Equals/GetHashCode/ToString,
//   so two anonymous objects with the same property names, types, order and values are equal
//   (Equals = true, same hash code). They are immutable (read-only properties) and can't be used as a
//   method return type or parameter in a typed way.
// - Class          => REFERENCE equality by default (Equals = false for two different objects).
// - Record         => VALUE equality too, but it is a real named type that can be used anywhere,
//                     and it supports 'with' expressions and positional syntax.

// ---------------------------------------------------------------------
// Part 5 — Extension Methods  (Q10)
// ---------------------------------------------------------------------
Console.WriteLine("\n===== Part 5: Extension Methods =====");

bool isShorter = "Stethoscope".IsShorterThan(5);
string repeated = "Ab".Repeat(4);

Console.WriteLine($"\"Stethoscope\".IsShorterThan(5) = {isShorter}");
Console.WriteLine($"\"Ab\".Repeat(4) = {repeated}");

// Bonus: calling it as a normal static method (before converting it to an extension method)
Console.WriteLine($"TextHelper.IsShorterThan(\"Stethoscope\", 5) = {TextHelper.IsShorterThan("Stethoscope", 5)}");

Console.WriteLine("\nDone.");


// =====================================================================
//  Types (must come AFTER the top-level statements)
// =====================================================================

// ---- Q1: class with primary constructor ----
public class Patient(int Id, string FullName, string PhoneNumber, string MedicalHistory)
{
    // Primary constructor parameters are not properties, so we expose them as properties
    // (needed by the mapper).
    public int Id { get; } = Id;
    public string FullName { get; } = FullName;
    public string PhoneNumber { get; } = PhoneNumber;
    public string MedicalHistory { get; } = MedicalHistory;

    public override string ToString()
        => $"Id: {Id} :: FullName: {FullName} :: PhoneNumber: {PhoneNumber} :: MedicalHistory: {MedicalHistory}";
}

// ---- Q3: positional record (no MedicalHistory) ----
public record PatientDto(int Id, string FullName, string PhoneNumber);

// ---- Q4: mapper ----
public static class PatientMapper
{
    public static PatientDto MapFromModelToDto(Patient patient)
        => new PatientDto(patient.Id, patient.FullName, patient.PhoneNumber);
}

// ---- Q5: Singleton ----
public class AppLogger
{
    private static AppLogger? _instance = null;

    private AppLogger()
    {
        Console.WriteLine("AppLogger instance created (this line prints only once)");
    }

    public static AppLogger GetLogger()
    {
        if (_instance is null)
            _instance = new AppLogger();

        return _instance;
    }
}

// ---- Q9: extension methods ----
public static class TextHelper
{
    public static bool IsShorterThan(this string value, int length)
    {
        return value.Length < length;
    }

    public static string Repeat(this string value, int times)
    {
        if (times < 0)
            throw new ArgumentOutOfRangeException(nameof(times), "times must be 0 or greater");

        StringBuilder sb = new();
        for (int i = 0; i < times; i++)
            sb.Append(value);

        return sb.ToString();
    }
}

// ---- Q11: FineCalculator (public so the test project can use it) ----
public class FineCalculator
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;

    public int Multiply(int a, int b) => a * b;

    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero.");

        return a / b;
    }
}
