using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWebAPI.Data
{
    public class TodoContext : DbContext
        
    {
        public TodoContext(DbContextOptions<TodoContext> options ) : base(options)
        {

        }
        public DbSet<ToDo> Todos { get; set; }
    }
}
