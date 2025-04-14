using KanBan_Model.Models;
using KanBan_Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanBan_Model.Areas.ToDo.Controllers
{
    [Area("ToDo")]
    public class ToDoController : Controller
    {
        private readonly ILogger<ToDoController> _logger;
        private KanBanService _client;

        public ToDoController(ILogger<ToDoController> logger, KanBanService client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index(int id)
        {
            var card = _client.LoadCardById(id);
            if (card is not null)
            {
                ViewData["Title"] = card.Title;
                _logger.LogInformation("The card is loaded");
                return View(card);
            }
            _logger.LogError("The card does not exist!");
            return NotFound();
        }

        [HttpPost]
        public IActionResult Index(KanBanCard model)
        {
            var card = _client.LoadCardById(model.Id);
            if (card is not null)
            {
                card.Title = model.Title;
                card.Description = model.Description;
                card.Assignee = model.Assignee;
                card.DueDate = model.DueDate;
                card.Phase = model.Phase;
                card.Priority = model.Priority;

                // card.Attachment!.Links = model.Attachment!.Links;
                // card.Attachment!.Document = card.Attachment!.Document;

                if (model.Attachment is not null)
                {
                    if (card.Attachment == null)
                    {
                        card.Attachment = new KanBanTaskAttachments();
                    }

                    card.Attachment.Links = model.Attachment.Links;
                    card.Attachment.Document = model.Attachment.Document;
                }

                _client.UpdateCard(model);

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return NotFound();
        }

    }
}
