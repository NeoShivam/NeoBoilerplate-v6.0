using AutoMapper;
using NeoBoilerplate.Application.Features.Categories.Commands.CreateCategory;
using NeoBoilerplate.Application.Features.Categories.Commands.StoredProcedure;
using NeoBoilerplate.Application.Features.Categories.Queries.GetCategoriesList;
using NeoBoilerplate.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using NeoBoilerplate.Application.Features.Events.Commands.CreateEvent;
using NeoBoilerplate.Application.Features.Events.Commands.Transaction;
using NeoBoilerplate.Application.Features.Events.Commands.UpdateEvent;
using NeoBoilerplate.Application.Features.Events.Queries.GetEventDetail;
using NeoBoilerplate.Application.Features.Events.Queries.GetEventsExport;
using NeoBoilerplate.Application.Features.Events.Queries.GetEventsList;
using NeoBoilerplate.Application.Features.Orders.GetOrdersForMonth;
using NeoBoilerplate.Domain.Entities;

namespace NeoBoilerplate.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {          
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, TransactionCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, StoredProcedureCommand>();
            CreateMap<Category, StoredProcedureDto>();

            CreateMap<Order, OrdersForMonthDto>();

            CreateMap<Event, EventListVm>().ConvertUsing<EventVmCustomMapper>();
        }
    }
}
