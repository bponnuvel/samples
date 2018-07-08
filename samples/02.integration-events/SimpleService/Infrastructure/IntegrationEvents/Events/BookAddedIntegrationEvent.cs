using HooliServices.BuildingBlocks.EventBus.Events;

namespace SimpleService.IntegrationEvents.Events
{
    public class BookAddedIntegrationEvent : IntegrationEvent
    {
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int YearOfPublish { get; set; }

        public BookAddedIntegrationEvent(int bookId, string author, string title, int yearofPublish)
        {
            BookId = bookId;
            Author = author;
            Title = title;
            YearOfPublish = yearofPublish;
        }

    }
}
