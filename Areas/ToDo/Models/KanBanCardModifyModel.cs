using System;
using KanBan_Model.Models;

namespace KanBan_Model.Areas.ToDo.Models;

public class KanBanCardModifyModel
{
    public int Id { get; set; }
    public KanBanCard? Card { get; set; }
}
