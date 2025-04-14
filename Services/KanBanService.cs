using KanBan_Model.Models;
namespace KanBan_Model.Services
{
    public class KanBanService
    {
        private static string connection_string = "";
        private readonly ILogger<KanBanService>? _logger;

        private List<KanBanCard> Cards { get; set; } = new();

        /// <summary>
        /// Adds a new card to the list
        /// </summary>
        /// <param name="card"></param>
        public void InsertNewCard(KanBanCard card){
            Cards.Add(card);
        }


        /// <summary>
        /// delete card, the task is completed
        /// </summary>
        /// <param name="id"></param>
        public void CloseById(int id)
        {
            var target = Cards.Where(x => x.Id == id).FirstOrDefault();
            if (target is not null)
            {
                Cards.Remove(target);
            }
        }

        public void UpdateCard(KanBanCard model)
        {
            var card = LoadCardById(model.Id);
            if (card is not null)
            {
                card.Title = model.Title;
                card.Description = model.Description;
                card.Assignee = model.Assignee;
                card.DueDate = model.DueDate;
                card.Phase = model.Phase;
            }
        }

        public void FillCards()
        {
            // Cards = new List<KanBanCard>();

            // Seed data
            for (int i = 1; i <= 5; i++)
            {
                Cards.Add(new KanBanCard
                {
                    Id = i,
                    Title = $"Task_{i}",
                    Description = $"Description of task {i}",
                    Assignee = $"Some Assignee {i}",
                    DueDate = DateTime.Now,
                    Phase = KanBanTaskPhase.To_Do
                });
            }

            for (int i = 6; i <= 10; i++)
            {
                Cards.Add(new KanBanCard
                {
                    Id = i,
                    Title = $"Task_{i}",
                    Description = $"Description of task {i}",
                    Assignee = $"Some Assignee {i}",
                    DueDate = DateTime.Now,
                    Phase = KanBanTaskPhase.In_Progress
                });
            }

            for (int i = 11; i <= 15; i++)
            {
                Cards.Add(new KanBanCard
                {
                    Id = i,
                    Title = $"Task_{i}",
                    Description = $"Description of task {i}",
                    Assignee = $"Some Assignee {i}",
                    DueDate = DateTime.Now,
                    Phase = KanBanTaskPhase.Testing
                });
            }

            for (int i = 16; i <= 20; i++)
            {
                Cards.Add(new KanBanCard
                {
                    Id = i,
                    Title = $"Task_{i}",
                    Description = $"Description of task {i}",
                    Assignee = $"Some Assignee {i}",
                    DueDate = DateTime.Now,
                    Phase = KanBanTaskPhase.Done
                });
            }
        }


        public KanBanService(ILogger<KanBanService>? logger)
        {
            _logger = logger;
            FillCards();
        }

        public List<KanBanCard> GetCards() => Cards;

        public KanBanCard? LoadCardById(int id)
        {
            return Cards.Where(card => card.Id == id).FirstOrDefault();
        }
    }

}