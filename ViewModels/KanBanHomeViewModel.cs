using KanBan_Model.Models;

namespace KanBan_Model.ViewModels;

public class KanBanCardViewModel
{
    public List<KanBanCard>? Cards { get; set; }
    public int Id { get; set; } // card id
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Assignee { get; set; } = string.Empty;
    public DateTime DueDate { get; set; } = DateTime.Now;
    public KanBanTaskPhase Phase { get; set; } = KanBanTaskPhase.To_Do;

}
