using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace EFCorePlusSQLiteUpdateIssue
{
    class Program
    {
        static void Main(string[] args)
        {
            var blogPosts = Enumerable.Range(0, 10).Select(i => new BlogPost());

            using (var db = new FooDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                void ListPosts()
                {
                    var inserted = db.BlogPosts.AsNoTracking().ToList().Select(x => x.ToString());
                    foreach (var value in inserted)
                    {
                        Console.WriteLine(value);
                    }
                }

                db.AddRange(blogPosts);
                db.SaveChanges();
                
                ListPosts();

                db.BlogPosts.Where(x => x.Id < 5).Update(x => new BlogPost
                {
                    ViewCount = x.ViewCount + 1
                });
                
                ListPosts();
            }
        }
    }
}