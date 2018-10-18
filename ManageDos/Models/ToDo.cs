using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageDos.Models
{
    public class ToDoList
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [Display(Name = "List Description")]
        [Required(AllowEmptyStrings = true)]
        public string ListDescription { get; set; }

        public List<ToDoItem> ListItem { get; set; }
        
        public VisibleStatus visible { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        //TODO: Add GroupID 
    }

    public class ToDoItem
    {
        [Key]
        public int ID { get; set; }

        public string Content { get; set; }

        [Display(Name = "Done")]
        public bool IsDone { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        public virtual ToDoList List { get; set; }

        public virtual ApplicationUser User { get; set; }

    }

    public enum VisibleStatus { Public, Group, Private }
}