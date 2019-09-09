using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Univer.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Header { get; set; }  // заголовок меню
        public string Url { get; set; } // адрес ссылки
        public int? Order { get; set; }  // порядок следования пункта в подменю
        public int? ParentId { get; set; }  // ссылка на id родительского меню
        public MenuItem Parent { get; set; }    // родительское меню

        public ICollection<MenuItem> Children { get; set; }   // дочерние пункты меню
        public MenuItem()
        {
            Children = new List<MenuItem>();
        }
    }

    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("FootballConnection") { }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}