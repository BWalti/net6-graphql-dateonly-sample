using HotChocolate.Data.Filters;

namespace graphql_minimal_api
{
    public class DateOnlyFilterInputType : FilterInputType<DateOnly>
    {
        protected override void Configure(
            IFilterInputTypeDescriptor<DateOnly> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(f => f.Year).Name("year");
            descriptor.Field(f => f.Month).Name("month");
            descriptor.Field(f => f.Day).Name("day");

            //descriptor.Operation(DefaultFilesOptions.Equals);
        }
    }
}