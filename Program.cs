using graphql_minimal_api;
using HotChocolate.Data.Filters;
using HotChocolate.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddScoped<AuthorRepository>()
  .AddScoped<BookRepository>()
  .AddDbContext<DemoDbContext>();

builder.Services
  .AddGraphQLServer()
  .AddQueryType<Query>()
  .AddMutationType<Mutation>()
  .AddFiltering() //configure => configure.BindRuntimeType<DateOnly, DateOnlyFilterInputType>())
  .AddSorting()
  .AddProjections()
  .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
  .BindRuntimeType<DateOnly, DateType>()
  .AddTypeConverter<DateTime, DateOnly>(from => DateOnly.FromDateTime(from))
  .AddTypeConverter<DateOnly, DateTime>(from => from.ToDateTime(default))
  .AddConvention<IFilterConvention>(new FilterConventionExtension(x =>
  {
      x.BindRuntimeType<DateOnly, ComparableOperationFilterInputType<DateOnly>>();
      x.BindRuntimeType<DateOnly?, ComparableOperationFilterInputType<DateOnly?>>();
      //x.BindRuntimeType<TimeOnly, ComparableOperationFilterInputType<TimeOnly>>();
      //x.BindRuntimeType<TimeOnly?, ComparableOperationFilterInputType<TimeOnly?>>();
  }))
  ;

var app = builder.Build();
var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DemoDbContext>();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

var scott = db.Add<Author>(new Author
{
    Id = new Guid("bfb9bdcc-60cd-4436-993e-86614355f5ff"),
    FirstName = "Scott",
    LastName = "Millett",
    Birthday = new DateOnly(1955, 2, 11)
});

var alberto = db.Add<Author>(new Author
{
    FirstName = "Alberto",
    LastName = "Brandolini",
    Birthday = new DateOnly(1955, 2, 11)
});

db.AddRange(
    new Book { Title = "Patterns, Principles, and Practices of Domain-Driven Design", PublishedInYear = 2022, Author = scott.Entity },
    new Book { Title = "The Accidental CIO: A Lean and Agile Playbook for IT Leaders", PublishedInYear = 2015, Author = scott.Entity },
    new Book { Title = "Real World .NET, C#, and Silverlight: Indispensible Experiences from 15 MVPs", PublishedInYear = 2011, Author = scott.Entity },
    new Book { Title = "Event Storming", PublishedInYear = 2022, Author = alberto.Entity }
    );

db.SaveChanges();

app.MapGraphQL();
app.Run();
