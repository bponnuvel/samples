using System;
using System.Threading.Tasks;
using HooliServices.BuildingBlocks.EventBus.Abstractions;
using SimpleService.Infrastructure;
using SimpleService.IntegrationEvents.Events;
using SimpleService.Model;

namespace SimpleService.IntegrationEvents.EventHandlers
{
    public class BookAddedIntegrationEventHandler :
        IIntegrationEventHandler<BookAddedIntegrationEvent>
    {
        AppDataContext _context;
        public BookAddedIntegrationEventHandler(AppDataContext appdataContext)
        {
            _context = appdataContext;
        }
        public async Task Handle(BookAddedIntegrationEvent @event)
        {
            var rating = new BookRating
            {
                BookId = @event.BookId.ToString(),
                BookTitle = @event.Title,
                Stars = GetRandomNumber(1, 5)
            };
            _context.BookRatings.Add(rating);

            await _context.SaveChangesAsync();
        }

        public float GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return (float)(random.NextDouble() * (maximum - minimum) + minimum);
        }
    }
}
