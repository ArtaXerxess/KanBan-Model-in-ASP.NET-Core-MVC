namespace KanBan_Model.Models;

/// <summary>
/// <para>A Simple data model of a KanBan Card that has the following properties</para>
/// <para>Id</para>
/// <para>Title</para>
/// <para>Assignee</para>
/// <para>DueDate</para>
/// <para>Phase</para> 
/// </summary>
public class KanBanCard
{
    // essentials
    public int Id { get; set; } // primary key, auto incrementing 
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Assignee { get; set; } = string.Empty;
    public DateTime DueDate { get; set; } = DateTime.Now;
    public KanBanTaskPhase Phase { get; set; } = KanBanTaskPhase.To_Do;
    // extras
    public KanBanTaskPriority Priority { get; set; } = KanBanTaskPriority.Low;
    public KanBanTaskAttachments? Attachment { get; set; }
}





/*
Essential Components:
    Title/Identifier: A concise name or code that uniquely identifies the work item. 
    Description: A brief summary of what needs to be done, including the goal and project scope. 
    Assignee: The individual or team responsible for completing the task. 
    Due Date: The deadline for completion. 
    Status: Indicates the current stage of the work item (e.g., To Do, In Progress, Done, Blocked). 

Additional Information:
    Subtasks: Break down larger tasks into smaller, actionable items. 
    Priority: Helps prioritize tasks and focus on the most important ones. 
    Attachments/Links: Can include documents, links to code repositories, or other relevant resources. 
    Cycle Time: The time it takes to complete a work item from start to finish. 
    Work-in-Progress (WIP) Limits: Specifies the maximum number of tasks that can be in a particular column at any given time. 
*/


