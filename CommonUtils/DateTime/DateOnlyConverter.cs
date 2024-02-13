using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace CommonUtils.DateTime;

public class DateOnlyConverter : ValueConverter<DateOnly, System.DateTime>
{
    public DateOnlyConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime))
    { }
}
