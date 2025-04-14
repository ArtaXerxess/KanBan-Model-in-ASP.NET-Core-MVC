using System.Diagnostics;
using KanBan_Model.Models;
using KanBan_Model.Services;
using KanBan_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KanBan_Model.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KanBanService _client;

        public List<KanBanCard>? Cards { get; set; }
        public HomeController(ILogger<HomeController> logger, KanBanService client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index()
        {
            var _viewModel = new KanBanCardViewModel();
            _viewModel.Cards = _client.GetCards();
            return View(_viewModel);
        }

        public IActionResult NewTask()
        {
            return RedirectToAction("Index", "NewKanBanTask", new { area = "NewKanBanTask" });
        }

        [HttpPost]
        public IActionResult CardAction(KanBanCardViewModel model, string action)
        {
            _logger.LogInformation("Card Form action triggered...");
            var card = _client.LoadCardById(model.Id);

            if (card is not null)
            {
                _logger.LogInformation("Card Found!");
                if (action == "modify")
                {
                    _logger.LogInformation("modify option triggered....");
                    return RedirectToAction("Index", "ToDo", new { area = "ToDo", id = card.Id });
                    // RedirectToAction("Index", "ToDo", new { area = "ToDo", id = 1 })

                }

                if (action == "close")
                {
                    _client.CloseById(card.Id);
                    _logger.LogInformation($"closed card : {card.Title}");
                }

                if (action == "move")
                {
                    switch (card.Phase)
                    {
                        case KanBanTaskPhase.To_Do:
                            card.Phase = KanBanTaskPhase.In_Progress;
                            _logger.LogInformation($"Moved card {card.Title} from {KanBanTaskPhase.To_Do.ToString()} to {KanBanTaskPhase.In_Progress.ToString()}");
                            break;
                        case KanBanTaskPhase.In_Progress:
                            card.Phase = KanBanTaskPhase.Testing;
                            _logger.LogInformation($"Moved card {card.Title} from {KanBanTaskPhase.In_Progress.ToString()} to {KanBanTaskPhase.Testing.ToString()}");
                            break;
                        case KanBanTaskPhase.Testing:
                            card.Phase = KanBanTaskPhase.Done;
                            _logger.LogInformation($"Moved card {card.Title} from {KanBanTaskPhase.Testing.ToString()} to {KanBanTaskPhase.Done.ToString()}");

                            break;
                        default:
                            _logger.LogInformation("such phase is not available...");
                            break;
                    }
                }

                Cards = _client.GetCards();
                return View("Index", new KanBanCardViewModel { Cards = Cards });
            }
            return NotFound("the card does not exist");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
