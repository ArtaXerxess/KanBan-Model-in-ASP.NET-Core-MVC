using KanBan_Model.Models;
using KanBan_Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanBan_Model.Areas.NewKanBanTask.Controllers
{
    [Area("NewKanBanTask")]
    public class NewKanBanTaskController : Controller
    {
        private KanBanService _client;
        private ILogger<NewKanBanTaskController> _logger;

        public NewKanBanTaskController(ILogger<NewKanBanTaskController> logger, KanBanService client)
        {
            _client = client;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewTask(KanBanCard card)
        {
            if (ModelState.IsValid)
            {
                var newCard = new KanBanCard();
                newCard.Title = card.Title;
                newCard.Description = card.Description;
                newCard.Assignee = card.Assignee;
                newCard.DueDate = card.DueDate;
                newCard.Phase = card.Phase;
                newCard.Priority = card.Priority;

                newCard.Attachment = new();

                if (card.Attachment is not null)
                {
                    newCard.Attachment.Links = card.Attachment.Links;
                    newCard.Attachment.Document = card.Attachment.Document;
                }

                _client.InsertNewCard(newCard);

                return RedirectToAction("Index", "Home", new { area = "" });

            }
            return View("Index");
        }
    }
}
